using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.CoreFinancial.Contract
{
    public class ContractMilestone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool IsCompleted => CompletedDate.HasValue;

        // Navigation property
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
    }
}
