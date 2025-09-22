using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models
{
    public class BudgetCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AllocatedAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal UtilizedAmount { get; set; }

        [Required]
        public int ProjectId { get; set; }

        // Navigation property
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}   