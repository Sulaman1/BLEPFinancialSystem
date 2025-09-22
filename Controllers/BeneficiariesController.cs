using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class BeneficiariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeneficiariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beneficiaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Beneficiaries.ToListAsync());
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Payments)
                .ThenInclude(p => p.Disbursement)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beneficiaries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CNIC,PhoneNumber,Email,Address,Type,BankAccountNumber,BankName")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CNIC,PhoneNumber,Email,Address,Type,BankAccountNumber,BankName")] Beneficiary beneficiary)
        {
            if (id != beneficiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.Id))
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
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiaryExists(int id)
        {
            return _context.Beneficiaries.Any(e => e.Id == id);
        }
    }
}