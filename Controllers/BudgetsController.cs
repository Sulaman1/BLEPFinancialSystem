using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.CoreFinancial.Budget;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var budgets = await _context.Budgets
                .Include(b => b.Project)
                .Include(b => b.LineItems)
                .ToListAsync();
            return View(budgets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var budget = await _context.Budgets
                .Include(b => b.Project)
                .Include(b => b.LineItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null) return NotFound();

            return View(budget);
        }

        public IActionResult Create()
        {
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Budget budget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Projects = _context.Projects.ToList();
            return View(budget);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return NotFound();

            ViewBag.Projects = _context.Projects.ToList();
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Budget budget)
        {
            if (id != budget.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Projects = _context.Projects.ToList();
            return View(budget);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var budget = await _context.Budgets
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null) return NotFound();

            return View(budget);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.Id == id);
        }
    }
}