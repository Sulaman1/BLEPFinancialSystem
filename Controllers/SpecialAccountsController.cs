using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.WBSpecificModules.SpecialAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class SpecialAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialAccounts.Include(s => s.Transactions).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.SpecialAccounts
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null) return NotFound();

            return View(account);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialAccount account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.SpecialAccounts.FindAsync(id);
            if (account == null) return NotFound();

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialAccount account)
        {
            if (id != account.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var account = await _context.SpecialAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null) return NotFound();

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.SpecialAccounts.FindAsync(id);
            _context.SpecialAccounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.SpecialAccounts.Any(e => e.Id == id);
        }
    }
}