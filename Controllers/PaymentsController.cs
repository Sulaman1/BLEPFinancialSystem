using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.Beneficiary).Include(p => p.Disbursement);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Beneficiary)
                .Include(p => p.Disbursement)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["BeneficiaryId"] = new SelectList(_context.Beneficiaries, "Id", "Name");
            ViewData["DisbursementId"] = new SelectList(_context.Disbursements, "Id", "ReferenceNumber");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentReference,Amount,PaymentDate,Description,Status,BeneficiaryId,DisbursementId,BankTransactionId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();

                // Update project's utilized funds if payment is completed
                if (payment.Status == PaymentStatus.Completed && payment.DisbursementId.HasValue)
                {
                    var disbursement = await _context.Disbursements
                        .Include(d => d.Project)
                        .FirstOrDefaultAsync(d => d.Id == payment.DisbursementId.Value);

                    if (disbursement != null && disbursement.Project != null)
                    {
                        disbursement.Project.FundsUtilized += payment.Amount;
                        _context.Update(disbursement.Project);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiaryId"] = new SelectList(_context.Beneficiaries, "Id", "Name", payment.BeneficiaryId);
            ViewData["DisbursementId"] = new SelectList(_context.Disbursements, "Id", "ReferenceNumber", payment.DisbursementId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["BeneficiaryId"] = new SelectList(_context.Beneficiaries, "Id", "Name", payment.BeneficiaryId);
            ViewData["DisbursementId"] = new SelectList(_context.Disbursements, "Id", "ReferenceNumber", payment.DisbursementId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentReference,Amount,PaymentDate,Description,Status,BeneficiaryId,DisbursementId,BankTransactionId")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get original payment for amount and status comparison
                    var originalPayment = await _context.Payments.AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id);

                    _context.Update(payment);
                    await _context.SaveChangesAsync();

                    // Update project's utilized funds if status changed to/from Completed
                    if (originalPayment != null && payment.DisbursementId.HasValue)
                    {
                        var disbursement = await _context.Disbursements
                            .Include(d => d.Project)
                            .FirstOrDefaultAsync(d => d.Id == payment.DisbursementId.Value);

                        if (disbursement != null && disbursement.Project != null)
                        {
                            if (originalPayment.Status != PaymentStatus.Completed && payment.Status == PaymentStatus.Completed)
                            {
                                // Status changed to Completed - add amount
                                disbursement.Project.FundsUtilized += payment.Amount;
                            }
                            else if (originalPayment.Status == PaymentStatus.Completed && payment.Status != PaymentStatus.Completed)
                            {
                                // Status changed from Completed - subtract amount
                                disbursement.Project.FundsUtilized -= payment.Amount;
                            }
                            else if (originalPayment.Status == PaymentStatus.Completed && payment.Status == PaymentStatus.Completed &&
                                     originalPayment.Amount != payment.Amount)
                            {
                                // Amount changed while status is Completed - adjust difference
                                disbursement.Project.FundsUtilized = disbursement.Project.FundsUtilized - originalPayment.Amount + payment.Amount;
                            }

                            _context.Update(disbursement.Project);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BeneficiaryId"] = new SelectList(_context.Beneficiaries, "Id", "Name", payment.BeneficiaryId);
            ViewData["DisbursementId"] = new SelectList(_context.Disbursements, "Id", "ReferenceNumber", payment.DisbursementId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Beneficiary)
                .Include(p => p.Disbursement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            // Update project's utilized funds if payment was completed
            if (payment.Status == PaymentStatus.Completed && payment.DisbursementId.HasValue)
            {
                var disbursement = await _context.Disbursements
                    .Include(d => d.Project)
                    .FirstOrDefaultAsync(d => d.Id == payment.DisbursementId.Value);

                if (disbursement != null && disbursement.Project != null)
                {
                    disbursement.Project.FundsUtilized -= payment.Amount;
                    _context.Update(disbursement.Project);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}