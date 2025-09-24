using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.CoreFinancial.Budget
{
    public class BudgetLineItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BudgetId { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } // WB Chart of Accounts

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ApprovedAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal RevisedAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal ActualExpenditure { get; set; }

        [DataType(DataType.Currency)]
        public decimal Commitments { get; set; }

        [DataType(DataType.Currency)]
        public decimal AvailableBalance { get; set; }

        // Navigation property
        [ForeignKey("BudgetId")]
        public virtual Budget Budget { get; set; }
    }
}
