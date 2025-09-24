using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace BLEPFinancialSystem.Models.CoreFinancial.Procurement
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProcurementPlanId { get; set; }

        [Required]
        [StringLength(200)]
        public string BidderName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal BidAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BidDate { get; set; }

        public BidStatus Status { get; set; } = BidStatus.Submitted;

        public string EvaluationRemarks { get; set; }

        // Navigation property
        [ForeignKey("ProcurementPlanId")]
        public virtual ProcurementPlan ProcurementPlan { get; set; }
    }
}
