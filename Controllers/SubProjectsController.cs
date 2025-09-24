using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Models.ProjectManagement.SubProjectManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    public class SubProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubProjects.Include(s => s.Components).ToListAsync());
        }

        // GET: SubProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .Include(s => s.Components)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // GET: SubProjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubProjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubProject subProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subProject);
        }

        // GET: SubProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects.FindAsync(id);
            if (subProject == null)
            {
                return NotFound();
            }
            return View(subProject);
        }

        // POST: SubProjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubProject subProject)
        {
            if (id != subProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubProjectExists(subProject.Id))
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
            return View(subProject);
        }

        // GET: SubProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // POST: SubProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subProject = await _context.SubProjects.FindAsync(id);
            _context.SubProjects.Remove(subProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubProjectExists(int id)
        {
            return _context.SubProjects.Any(e => e.Id == id);
        }
    }
}