using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.CoreFinancial.Contract
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ContractNo { get; set; }

        [Required]
        [StringLength(200)]
        public string Contractor { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ContractAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<ContractMilestone> Milestones { get; set; } = new HashSet<ContractMilestone>();
        public virtual ICollection<ContractPayment> Payments { get; set; } = new HashSet<ContractPayment>();
    }

}
