using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.WBSpecificModules.SpecialAccount
{
    public class SpecialAccountTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SpecialAccountId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Reference { get; set; }

        // Navigation property
        [ForeignKey("SpecialAccountId")]
        public virtual SpecialAccount SpecialAccount { get; set; }
    }
}
