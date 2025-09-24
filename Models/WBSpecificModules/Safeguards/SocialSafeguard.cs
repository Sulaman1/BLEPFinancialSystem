using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.WBSpecificModules.Safeguards
{
    public class SocialSafeguard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        public string AssessmentReport { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AssessmentDate { get; set; }

        public ComplianceStatus Status { get; set; } = ComplianceStatus.UnderReview;
    }
}
