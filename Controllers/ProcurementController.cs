using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.CoreFinancial.Procurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class ProcurementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcurementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _context.ProcurementPlans.ToListAsync();
            return View(plans);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.ProcurementPlans
                .Include(p => p.Bids)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null) return NotFound();

            return View(plan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcurementPlan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.ProcurementPlans.FindAsync(id);
            if (plan == null) return NotFound();

            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcurementPlan plan)
        {
            if (id != plan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcurementPlanExists(plan.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var plan = await _context.ProcurementPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null) return NotFound();

            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.ProcurementPlans.FindAsync(id);
            _context.ProcurementPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcurementPlanExists(int id)
        {
            return _context.ProcurementPlans.Any(e => e.Id == id);
        }
    }
}