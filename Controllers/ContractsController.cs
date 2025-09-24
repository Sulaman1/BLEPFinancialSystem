using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models.CoreFinancial.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Milestones)
                .Include(c => c.Payments)
                .ToListAsync();
            return View(contracts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Milestones)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null) return NotFound();

            return View(contract);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();

            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract)
        {
            if (id != contract.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null) return NotFound();

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}