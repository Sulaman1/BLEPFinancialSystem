using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.WBSpecificModules.Withdrawal
{
    public class WithdrawalApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ApplicationNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountRequested { get; set; }

        [Required]
        public string Purpose { get; set; }

        public WBApplicationStatus Status { get; set; } = WBApplicationStatus.Draft;

        public virtual ICollection<SupportingDocument> Documents { get; set; } = new HashSet<SupportingDocument>();
    }
}
