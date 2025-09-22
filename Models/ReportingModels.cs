using System;
using System.Collections.Generic;

namespace BLEPFinancialSystem.Models
{
    public class FinancialSummaryReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalDisbursed { get; set; }
        public decimal TotalUtilized { get; set; }
        public decimal UtilizationRate { get; set; }
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int CompletedProjects { get; set; }
        public List<BudgetCategorySummary> CategorySummaries { get; set; }
    }

    public class BudgetCategorySummary
    {
        public string CategoryName { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal UtilizedAmount { get; set; }
        public decimal UtilizationRate { get; set; }
    }

    public class DisbursementReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DisbursementSummary> Disbursements { get; set; }
        public decimal TotalDisbursed { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<DisbursementType, decimal> DisbursementByType { get; set; }
        public Dictionary<DisbursementStatus, int> DisbursementByStatus { get; set; }
    }

    public class DisbursementSummary
    {
        public string ReferenceNumber { get; set; }
        public DateTime DisbursementDate { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public DisbursementType Type { get; set; }
        public DisbursementStatus Status { get; set; }
        public string ProjectName { get; set; }
    }

    public class BeneficiaryPaymentReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<BeneficiaryPaymentSummary> Payments { get; set; }
        public decimal TotalPayments { get; set; }
        public int TotalBeneficiaries { get; set; }
        public Dictionary<BeneficiaryType, decimal> PaymentsByBeneficiaryType { get; set; }
        public Dictionary<PaymentStatus, int> PaymentsByStatus { get; set; }
    }

    public class BeneficiaryPaymentSummary
    {
        public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public PaymentStatus Status { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryCNIC { get; set; }
        public BeneficiaryType BeneficiaryType { get; set; }
        public string ProjectName { get; set; }
    }

    public class AuditReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AuditLogSummary> Logs { get; set; }
        public int TotalLogs { get; set; }
        public Dictionary<string, int> ActivitiesByUser { get; set; }
        public Dictionary<string, int> ActivitiesByEntity { get; set; }
    }

    public class AuditLogSummary
    {
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
    }

    public class ReportParameters
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ProjectId { get; set; }
        public int? BeneficiaryId { get; set; }
        public DisbursementType? DisbursementType { get; set; }
        public BeneficiaryType? BeneficiaryType { get; set; }
        public string ReportFormat { get; set; } = "HTML"; // HTML, PDF, Excel
    }
}