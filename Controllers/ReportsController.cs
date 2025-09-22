using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLEPFinancialSystem.Services;
using BLEPFinancialSystem.Models;
using System;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Controllers
{
    //[Authorize(Roles = "WorldBank,ProjectDirector,FinanceOfficer")]
    public class ReportsController : Controller
    {
        private readonly IReportingService _reportingService;

        public ReportsController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FinancialSummary()
        {
            var parameters = new ReportParameters
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };
            return View(parameters);
        }

        [HttpPost]
        public async Task<IActionResult> FinancialSummary(ReportParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }

            var report = await _reportingService.GenerateFinancialSummaryReport(parameters);

            if (parameters.ReportFormat == "PDF")
            {
                var pdfBytes = await _reportingService.ExportReportToPdf(report, "FinancialSummary");
                return File(pdfBytes, "application/pdf", $"FinancialSummary_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            else if (parameters.ReportFormat == "Excel")
            {
                var excelBytes = await _reportingService.ExportReportToExcel(report, "FinancialSummary");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"FinancialSummary_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }

            ViewBag.Report = report;
            return View(parameters);
        }

        [HttpGet]
        public IActionResult Disbursements()
        {
            var parameters = new ReportParameters
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };
            return View(parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Disbursements(ReportParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }

            var report = await _reportingService.GenerateDisbursementReport(parameters);

            if (parameters.ReportFormat == "PDF")
            {
                var pdfBytes = await _reportingService.ExportReportToPdf(report, "Disbursements");
                return File(pdfBytes, "application/pdf", $"Disbursements_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            else if (parameters.ReportFormat == "Excel")
            {
                var excelBytes = await _reportingService.ExportReportToExcel(report, "Disbursements");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Disbursements_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }

            ViewBag.Report = report;
            return View(parameters);
        }

        [HttpGet]
        public IActionResult BeneficiaryPayments()
        {
            var parameters = new ReportParameters
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };
            return View(parameters);
        }

        [HttpPost]
        public async Task<IActionResult> BeneficiaryPayments(ReportParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }

            var report = await _reportingService.GenerateBeneficiaryPaymentReport(parameters);

            if (parameters.ReportFormat == "PDF")
            {
                var pdfBytes = await _reportingService.ExportReportToPdf(report, "BeneficiaryPayments");
                return File(pdfBytes, "application/pdf", $"BeneficiaryPayments_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            else if (parameters.ReportFormat == "Excel")
            {
                var excelBytes = await _reportingService.ExportReportToExcel(report, "BeneficiaryPayments");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"BeneficiaryPayments_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }

            ViewBag.Report = report;
            return View(parameters);
        }

        [HttpGet]
        [Authorize(Roles = "WorldBank,ProjectDirector")]
        public IActionResult AuditLogs()
        {
            var parameters = new ReportParameters
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };
            return View(parameters);
        }

        [HttpPost]
        [Authorize(Roles = "WorldBank,ProjectDirector")]
        public async Task<IActionResult> AuditLogs(ReportParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return View(parameters);
            }

            var report = await _reportingService.GenerateAuditReport(parameters);

            if (parameters.ReportFormat == "PDF")
            {
                var pdfBytes = await _reportingService.ExportReportToPdf(report, "AuditLogs");
                return File(pdfBytes, "application/pdf", $"AuditLogs_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            else if (parameters.ReportFormat == "Excel")
            {
                var excelBytes = await _reportingService.ExportReportToExcel(report, "AuditLogs");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AuditLogs_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            }

            ViewBag.Report = report;
            return View(parameters);
        }
    }
}