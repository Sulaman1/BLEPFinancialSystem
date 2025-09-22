using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using BLEPFinancialSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BLEPFinancialSystem.Controllers
{
    //[Authorize(Roles = "FinanceOfficer,ProjectDirector")]
    public class ChequesController : Controller
    {
        private readonly IChequeService _chequeService;
        private readonly ApplicationDbContext _context;

        public ChequesController(IChequeService chequeService, ApplicationDbContext context)
        {
            _chequeService = chequeService;
            _context = context;
        }

        // GET: Cheques
        public async Task<IActionResult> Index()
        {
            var cheques = await _chequeService.GetChequesAsync();
            return View(cheques);
        }

        // GET: Cheques/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cheque = await _chequeService.GetChequeAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }
            return View(cheque);
        }

        // GET: Cheques/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Templates = await _chequeService.GetTemplatesAsync();
            ViewBag.BankAccounts = await _context.BankAccounts.Where(b => b.IsActive).ToListAsync();
            ViewBag.Payments = await _context.Payments
                .Include(p => p.Beneficiary)
                .Where(p => p.Status == PaymentStatus.Approved)
                .ToListAsync();

            return View();
        }

        // POST: Cheques/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cheque cheque)
        {
            if (ModelState.IsValid)
            {
                cheque.CreatedBy = User.Identity.Name;
                cheque = await _chequeService.CreateChequeAsync(cheque);
                return RedirectToAction(nameof(Details), new { id = cheque.Id });
            }

            ViewBag.Templates = await _chequeService.GetTemplatesAsync();
            ViewBag.BankAccounts = await _context.BankAccounts.Where(b => b.IsActive).ToListAsync();
            ViewBag.Payments = await _context.Payments
                .Include(p => p.Beneficiary)
                .Where(p => p.Status == PaymentStatus.Approved)
                .ToListAsync();

            return View(cheque);
        }

        // GET: Cheques/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cheque = await _chequeService.GetChequeAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            ViewBag.Templates = await _chequeService.GetTemplatesAsync();
            ViewBag.BankAccounts = await _context.BankAccounts.Where(b => b.IsActive).ToListAsync();
            ViewBag.Payments = await _context.Payments
                .Include(p => p.Beneficiary)
                .Where(p => p.Status == PaymentStatus.Approved)
                .ToListAsync();

            return View(cheque);
        }

        // POST: Cheques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cheque cheque)
        {
            if (id != cheque.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _chequeService.UpdateChequeAsync(cheque);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Templates = await _chequeService.GetTemplatesAsync();
            ViewBag.BankAccounts = await _context.BankAccounts.Where(b => b.IsActive).ToListAsync();
            ViewBag.Payments = await _context.Payments
                .Include(p => p.Beneficiary)
                .Where(p => p.Status == PaymentStatus.Approved)
                .ToListAsync();

            return View(cheque);
        }

        // GET: Cheques/Print/5
        public async Task<IActionResult> Print(int id)
        {
            var cheque = await _chequeService.GetChequeAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            var template = await _chequeService.GetTemplateAsync(cheque.TemplateId);
            var bankAccount = await _context.BankAccounts.FindAsync(cheque.BankAccountId);

            var printModel = new ChequePrintModel
            {
                Cheque = cheque,
                Template = template,
                BankAccount = bankAccount
            };

            return View(printModel);
        }

        // POST: Cheques/MarkPrinted/5
        [HttpPost]
        public async Task<IActionResult> MarkPrinted(int id)
        {
            var cheque = await _chequeService.GetChequeAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            cheque.Status = ChequeStatus.Printed;
            cheque.PrintedDate = DateTime.Now;
            await _chequeService.UpdateChequeAsync(cheque);

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Cheques/Templates
        public async Task<IActionResult> Templates()
        {
            var templates = await _chequeService.GetTemplatesAsync();
            return View(templates);
        }

        // GET: Cheques/CreateTemplate
        public IActionResult CreateTemplate()
        {
            return View();
        }

        // POST: Cheques/CreateTemplate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTemplate(ChequeTemplate template)
        {
            if (ModelState.IsValid)
            {
                template.CreatedBy = User.Identity.Name;
                await _chequeService.CreateTemplateAsync(template);
                return RedirectToAction(nameof(Templates));
            }
            return View(template);
        }

        // GET: Cheques/EditTemplate/5
        public async Task<IActionResult> EditTemplate(int id)
        {
            var template = await _chequeService.GetTemplateAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Cheques/EditTemplate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTemplate(int id, ChequeTemplate template)
        {
            if (id != template.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    template.ModifiedDate = DateTime.Now;
                    await _chequeService.UpdateTemplateAsync(template);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Templates));
            }
            return View(template);
        }

        // POST: Cheques/ConvertAmountToWords
        [HttpPost]
        public async Task<IActionResult> ConvertAmountToWords([FromBody] decimal amount)
        {
            var words = await _chequeService.ConvertAmountToWords(amount);
            return Json(new { amountInWords = words });
        }
    }
}