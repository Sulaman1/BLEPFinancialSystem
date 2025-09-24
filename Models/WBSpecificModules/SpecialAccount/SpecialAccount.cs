using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.WBSpecificModules.SpecialAccount
{
    public class SpecialAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal CurrentBalance { get; set; }

        [DataType(DataType.Currency)]
        public decimal ReconciliationBalance { get; set; }

        public virtual ICollection<SpecialAccountTransaction> Transactions { get; set; } = new HashSet<SpecialAccountTransaction>();
    }
}
