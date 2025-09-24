using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class WithdrawalApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WithdrawalApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.WithdrawalApplications.Include(w => w.Documents).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.WithdrawalApplications
                .Include(w => w.Documents)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null) return NotFound();

            return View(application);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WithdrawalApplication application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.WithdrawalApplications.FindAsync(id);
            if (application == null) return NotFound();

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WithdrawalApplication application)
        {
            if (id != application.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.WithdrawalApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null) return NotFound();

            return View(application);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.WithdrawalApplications.FindAsync(id);
            _context.WithdrawalApplications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.WithdrawalApplications.Any(e => e.Id == id);
        }
    }
}