using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

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

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public int BeneficiaryId { get; set; }

        public int? DisbursementId { get; set; }

        public string BankTransactionId { get; set; }

        // Navigation properties
        [ForeignKey("BeneficiaryId")]
        public virtual Beneficiary Beneficiary { get; set; }

        [ForeignKey("DisbursementId")]
        public virtual Disbursement Disbursement { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Approved,
        Processed,
        Completed,
        Rejected,
        Failed
    }
}