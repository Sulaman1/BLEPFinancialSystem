using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal
{
    public class SupportingDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WithdrawalApplicationId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required]
        public string FilePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("WithdrawalApplicationId")]
        public virtual WithdrawalApplication WithdrawalApplication { get; set; }
    }

}
