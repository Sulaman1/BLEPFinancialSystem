using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class DisbursementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisbursementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disbursements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Disbursements.Include(d => d.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Disbursements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disbursement = await _context.Disbursements
                .Include(d => d.Project)
                .Include(d => d.Payments)
                .ThenInclude(p => p.Beneficiary)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (disbursement == null)
            {
                return NotFound();
            }

            return View(disbursement);
        }

        // GET: Disbursements/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: Disbursements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReferenceNumber,Amount,DisbursementDate,Purpose,Type,ProjectId,Source,BankAccountDetails,Status")] Disbursement disbursement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disbursement);
                await _context.SaveChangesAsync();

                // Update project's disbursed funds
                var project = await _context.Projects.FindAsync(disbursement.ProjectId);
                if (project != null)
                {
                    project.FundsDisbursed += disbursement.Amount;
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", disbursement.ProjectId);
            return View(disbursement);
        }

        // GET: Disbursements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disbursement = await _context.Disbursements.FindAsync(id);
            if (disbursement == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", disbursement.ProjectId);
            return View(disbursement);
        }

        // POST: Disbursements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferenceNumber,Amount,DisbursementDate,Purpose,Type,ProjectId,Source,BankAccountDetails,Status")] Disbursement disbursement)
        {
            if (id != disbursement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get original amount for project fund adjustment
                    var originalDisbursement = await _context.Disbursements.AsNoTracking()
                        .FirstOrDefaultAsync(d => d.Id == id);

                    _context.Update(disbursement);
                    await _context.SaveChangesAsync();

                    // Update project's disbursed funds if amount changed
                    if (originalDisbursement != null && originalDisbursement.Amount != disbursement.Amount)
                    {
                        var project = await _context.Projects.FindAsync(disbursement.ProjectId);
                        if (project != null)
                        {
                            project.FundsDisbursed = project.FundsDisbursed - originalDisbursement.Amount + disbursement.Amount;
                            _context.Update(project);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisbursementExists(disbursement.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", disbursement.ProjectId);
            return View(disbursement);
        }

        // GET: Disbursements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disbursement = await _context.Disbursements
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disbursement == null)
            {
                return NotFound();
            }

            return View(disbursement);
        }

        // POST: Disbursements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disbursement = await _context.Disbursements.FindAsync(id);
            _context.Disbursements.Remove(disbursement);
            await _context.SaveChangesAsync();

            // Update project's disbursed funds
            var project = await _context.Projects.FindAsync(disbursement.ProjectId);
            if (project != null)
            {
                project.FundsDisbursed -= disbursement.Amount;
                _context.Update(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DisbursementExists(int id)
        {
            return _context.Disbursements.Any(e => e.Id == id);
        }
    }
}