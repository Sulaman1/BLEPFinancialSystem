using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models
{
    public class Disbursement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DisbursementDate { get; set; }

        public string Purpose { get; set; }

        [Required]
        public DisbursementType Type { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public string Source { get; set; }

        public string BankAccountDetails { get; set; }

        public DisbursementStatus Status { get; set; }

        // Navigation properties
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public Disbursement()
        {
            Payments = new HashSet<Payment>();
        }
    }

    public enum DisbursementType
    {
        SpecialAccount,
        ImprestAccount,
        DirectTransfer
    }

    public enum DisbursementStatus
    {
        Pending,
        Approved,
        Rejected,
        Processed,
        Completed
    }
}