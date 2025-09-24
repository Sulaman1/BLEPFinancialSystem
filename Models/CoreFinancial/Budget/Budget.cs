using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.CoreFinancial.Budget
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int FiscalYear { get; set; }

        public BudgetStatus Status { get; set; } = BudgetStatus.Draft;

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public virtual ICollection<BudgetLineItem> LineItems { get; set; } = new HashSet<BudgetLineItem>();
    }
}
