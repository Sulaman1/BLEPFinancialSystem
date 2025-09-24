using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.WBSpecificModules.Safeguards
{
    public class EnvironmentalSafeguard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string AssessmentType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AssessmentDate { get; set; }

        [Required]
        public string Findings { get; set; }

        public string MitigationMeasures { get; set; }

        public ComplianceStatus Status { get; set; } = ComplianceStatus.UnderReview;
    }
}
