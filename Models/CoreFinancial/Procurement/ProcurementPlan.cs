using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BLEPFinancialSystem.Models.CoreFinancial.Procurement
{
    public class ProcurementPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNo { get; set; }

        public ProcurementMethod Method { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal EstimatedAmount { get; set; }

        public ProcurementStatus Status { get; set; } = ProcurementStatus.Planning;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

        public virtual ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();
    }
}
