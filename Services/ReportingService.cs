using Microsoft.EntityFrameworkCore;
using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Services
{
    public interface IReportingService
    {
        Task<FinancialSummaryReport> GenerateFinancialSummaryReport(ReportParameters parameters);
        Task<DisbursementReport> GenerateDisbursementReport(ReportParameters parameters);
        Task<BeneficiaryPaymentReport> GenerateBeneficiaryPaymentReport(ReportParameters parameters);
        Task<AuditReport> GenerateAuditReport(ReportParameters parameters);
        Task<byte[]> ExportReportToPdf(object report, string reportType);
        Task<byte[]> ExportReportToExcel(object report, string reportType);
    }

    public class ReportingService : IReportingService
    {
        private readonly ApplicationDbContext _context;

        public ReportingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialSummaryReport> GenerateFinancialSummaryReport(ReportParameters parameters)
        {
            var projectsQuery = _context.Projects.AsQueryable();

            // Apply date filter if provided
            if (parameters.StartDate != default && parameters.EndDate != default)
            {
                projectsQuery = projectsQuery.Where(p =>
                    (p.StartDate >= parameters.StartDate && p.StartDate <= parameters.EndDate) ||
                    (p.EndDate >= parameters.StartDate && p.EndDate <= parameters.EndDate) ||
                    (p.StartDate <= parameters.StartDate && p.EndDate >= parameters.EndDate));
            }

            // Apply project filter if provided
            if (parameters.ProjectId.HasValue)
            {
                projectsQuery = projectsQuery.Where(p => p.Id == parameters.ProjectId.Value);
            }

            var projects = await projectsQuery.ToListAsync();
            var budgetCategories = await _context.BudgetCategories
                .Where(bc => projects.Select(p => p.Id).Contains(bc.ProjectId))
                .ToListAsync();

            var report = new FinancialSummaryReport
            {
                StartDate = parameters.StartDate,
                EndDate = parameters.EndDate,
                TotalBudget = projects.Sum(p => p.TotalBudget),
                TotalDisbursed = projects.Sum(p => p.FundsDisbursed),
                TotalUtilized = projects.Sum(p => p.FundsUtilized),
                TotalProjects = projects.Count,
                ActiveProjects = projects.Count(p => p.Status == ProjectStatus.Active),
                CompletedProjects = projects.Count(p => p.Status == ProjectStatus.Completed || p.Status == ProjectStatus.Closed),
                CategorySummaries = new List<BudgetCategorySummary>()
            };

            // Calculate utilization rate
            report.UtilizationRate = report.TotalBudget > 0 ?
                (report.TotalUtilized / report.TotalBudget) * 100 : 0;

            // Add category summaries
            foreach (var category in budgetCategories.GroupBy(bc => bc.Name))
            {
                var allocated = category.Sum(c => c.AllocatedAmount);
                var utilized = category.Sum(c => c.UtilizedAmount);
                var utilizationRate = allocated > 0 ? (utilized / allocated) * 100 : 0;

                report.CategorySummaries.Add(new BudgetCategorySummary
                {
                    CategoryName = category.Key,
                    AllocatedAmount = allocated,
                    UtilizedAmount = utilized,
                    UtilizationRate = utilizationRate
                });
            }

            return report;
        }

        public async Task<DisbursementReport> GenerateDisbursementReport(ReportParameters parameters)
        {
            var disbursementsQuery = _context.Disbursements
                .Include(d => d.Project)
                .AsQueryable();

            // Apply date filter
            if (parameters.StartDate != default && parameters.EndDate != default)
            {
                disbursementsQuery = disbursementsQuery.Where(d =>
                    d.DisbursementDate >= parameters.StartDate &&
                    d.DisbursementDate <= parameters.EndDate);
            }

            // Apply project filter
            if (parameters.ProjectId.HasValue)
            {
                disbursementsQuery = disbursementsQuery.Where(d => d.ProjectId == parameters.ProjectId.Value);
            }

            // Apply disbursement type filter
            if (parameters.DisbursementType.HasValue)
            {
                disbursementsQuery = disbursementsQuery.Where(d => d.Type == parameters.DisbursementType.Value);
            }

            var disbursements = await disbursementsQuery.ToListAsync();

            var report = new DisbursementReport
            {
                StartDate = parameters.StartDate,
                EndDate = parameters.EndDate,
                Disbursements = disbursements.Select(d => new DisbursementSummary
                {
                    ReferenceNumber = d.ReferenceNumber,
                    DisbursementDate = d.DisbursementDate,
                    Amount = d.Amount,
                    Purpose = d.Purpose,
                    Type = d.Type,
                    Status = d.Status,
                    ProjectName = d.Project?.Name
                }).ToList(),
                TotalDisbursed = disbursements.Sum(d => d.Amount),
                TotalCount = disbursements.Count,
                DisbursementByType = disbursements
                    .GroupBy(d => d.Type)
                    .ToDictionary(g => g.Key, g => g.Sum(d => d.Amount)),
                DisbursementByStatus = disbursements
                    .GroupBy(d => d.Status)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return report;
        }

        public async Task<BeneficiaryPaymentReport> GenerateBeneficiaryPaymentReport(ReportParameters parameters)
        {
            var paymentsQuery = _context.Payments
                .Include(p => p.Beneficiary)
                .Include(p => p.Disbursement)
                .ThenInclude(d => d.Project)
                .AsQueryable();

            // Apply date filter
            if (parameters.StartDate != default && parameters.EndDate != default)
            {
                paymentsQuery = paymentsQuery.Where(p =>
                    p.PaymentDate >= parameters.StartDate &&
                    p.PaymentDate <= parameters.EndDate);
            }

            // Apply beneficiary filter
            if (parameters.BeneficiaryId.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.BeneficiaryId == parameters.BeneficiaryId.Value);
            }

            // Apply beneficiary type filter
            if (parameters.BeneficiaryType.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Beneficiary.Type == parameters.BeneficiaryType.Value);
            }

            // Apply project filter through disbursement
            if (parameters.ProjectId.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Disbursement.ProjectId == parameters.ProjectId.Value);
            }

            var payments = await paymentsQuery.ToListAsync();
            var beneficiaries = payments.Select(p => p.Beneficiary).Distinct().ToList();

            var report = new BeneficiaryPaymentReport
            {
                StartDate = parameters.StartDate,
                EndDate = parameters.EndDate,
                Payments = payments.Select(p => new BeneficiaryPaymentSummary
                {
                    PaymentReference = p.PaymentReference,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    Description = p.Description,
                    Status = p.Status,
                    BeneficiaryName = p.Beneficiary?.Name,
                    BeneficiaryCNIC = p.Beneficiary?.CNIC,
                    BeneficiaryType = p.Beneficiary?.Type ?? BeneficiaryType.Individual,
                    ProjectName = p.Disbursement?.Project?.Name
                }).ToList(),
                TotalPayments = payments.Sum(p => p.Amount),
                TotalBeneficiaries = beneficiaries.Count,
                PaymentsByBeneficiaryType = payments
                    .GroupBy(p => p.Beneficiary.Type)
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.Amount)),
                PaymentsByStatus = payments
                    .GroupBy(p => p.Status)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return report;
        }

        public async Task<AuditReport> GenerateAuditReport(ReportParameters parameters)
        {
            var auditLogsQuery = _context.AuditLogs.AsQueryable();

            // Apply date filter
            if (parameters.StartDate != default && parameters.EndDate != default)
            {
                auditLogsQuery = auditLogsQuery.Where(a =>
                    a.Timestamp >= parameters.StartDate &&
                    a.Timestamp <= parameters.EndDate);
            }

            var auditLogs = await auditLogsQuery.ToListAsync();

            var report = new AuditReport
            {
                StartDate = parameters.StartDate,
                EndDate = parameters.EndDate,
                Logs = auditLogs.Select(a => new AuditLogSummary
                {
                    UserId = a.UserId,
                    Action = a.Action,
                    Entity = a.Entity,
                    EntityId = a.EntityId,
                    Timestamp = a.Timestamp,
                    IpAddress = a.IpAddress
                }).ToList(),
                TotalLogs = auditLogs.Count,
                ActivitiesByUser = auditLogs
                    .GroupBy(a => a.UserId)
                    .ToDictionary(g => g.Key, g => g.Count()),
                ActivitiesByEntity = auditLogs
                    .GroupBy(a => a.Entity)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return report;
        }

        public async Task<byte[]> ExportReportToPdf(object report, string reportType)
        {
            // Implementation for PDF export using a library like iTextSharp or QuestPDF
            // This is a placeholder implementation
            await Task.Delay(100); // Simulate processing time
            return new byte[0];
        }

        public async Task<byte[]> ExportReportToExcel(object report, string reportType)
        {
            // Implementation for Excel export using a library like EPPlus or ClosedXML
            // This is a placeholder implementation
            await Task.Delay(100); // Simulate processing time
            return new byte[0];
        }
    }
}