namespace BLEPFinancialSystem.Models
{
    public enum BudgetStatus
    {
        Draft,
        Submitted,
        Approved,
        Revised,
        Closed
    }

    public enum ProcurementMethod
    {
        NationalCompetitiveBidding,
        InternationalCompetitiveBidding,
        Shopping,
        DirectContracting,
        CommunityParticipation
    }

    public enum ProcurementStatus
    {
        Planning,
        Advertisement,
        Bidding,
        Evaluation,
        Awarded,
        ContractSigned,
        Completed,
        Cancelled
    }

    public enum BidStatus
    {
        Submitted,
        UnderEvaluation,
        TechnicallyQualified,
        TechnicallyDisqualified,
        FinanciallyQualified,
        Awarded,
        Rejected
    }

    public enum ComplianceStatus
    {
        Compliant,
        NonCompliant,
        PartiallyCompliant,
        UnderReview,
        NotApplicable
    }

    public enum WBApplicationStatus
    {
        Draft,
        Submitted,
        UnderReview,
        Approved,
        Rejected,
        Paid
    }

    public enum SubProjectStatus
    {
        Proposed,
        Approved,
        Implementation,
        Completed,
        Suspended,
        Terminated
    }
}