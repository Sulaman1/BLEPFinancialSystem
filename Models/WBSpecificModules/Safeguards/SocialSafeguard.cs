namespace BLEPFinancialSystem.Models.WBSpecificModules.Safeguards
{
    public class SocialSafeguard
    {
        public int Id { get; set; }
        public string Category { get; set; } // Resettlement, Indigenous Peoples, etc.
        public string AssessmentReport { get; set; }
        public DateTime AssessmentDate { get; set; }
        public ComplianceStatus Status { get; set; }
    }
}
