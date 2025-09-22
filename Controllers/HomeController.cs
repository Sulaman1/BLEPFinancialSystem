using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLEPFinancialSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BLEPFinancialSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get basic statistics for dashboard
            var totalProjects = await _context.Projects.CountAsync();
            var totalBeneficiaries = await _context.Beneficiaries.CountAsync();
            var totalPayments = await _context.Payments.CountAsync();
            var totalCheques = await _context.Cheques.CountAsync();

            ViewBag.TotalProjects = totalProjects;
            ViewBag.TotalBeneficiaries = totalBeneficiaries;
            ViewBag.TotalPayments = totalPayments;
            ViewBag.TotalCheques = totalCheques;

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}