namespace BLEPFinancialSystem.Models.WBSpecificModules.Safeguards
{
    public class EnvironmentalSafeguard
    {
        public int Id { get; set; }
        public string AssessmentType { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string Findings { get; set; }
        public string MitigationMeasures { get; set; }
        public ComplianceStatus Status { get; set; }
    }
}
