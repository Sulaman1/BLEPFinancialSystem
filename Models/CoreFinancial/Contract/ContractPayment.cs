using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.CoreFinancial.Contract
{
    public class ContractPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentReference { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        public string Description { get; set; }

        // Navigation property
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
    }
}
