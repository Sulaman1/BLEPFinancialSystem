using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models
{
    public class ChequeTemplate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TemplateName { get; set; }

        [Required]
        public string Description { get; set; }

        // Dimensions
        public decimal Width { get; set; } = 8.0m; // inches
        public decimal Height { get; set; } = 3.5m; // inches

        // Font settings
        public string FontFamily { get; set; } = "Arial";
        public int FontSize { get; set; } = 12;

        // Position settings for cheque elements
        public decimal PayeeNameX { get; set; }
        public decimal PayeeNameY { get; set; }
        public decimal AmountX { get; set; }
        public decimal AmountY { get; set; }
        public decimal AmountWordsX { get; set; }
        public decimal AmountWordsY { get; set; }
        public decimal DateX { get; set; }
        public decimal DateY { get; set; }
        public decimal SignatureX { get; set; }
        public decimal SignatureY { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }
    }

    public class Cheque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ChequeNumber { get; set; }

        [Required]
        public int BankAccountId { get; set; }

        [Required]
        [StringLength(200)]
        public string PayeeName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public string AmountInWords { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        public string Memo { get; set; }

        [Required]
        public int TemplateId { get; set; }

        public int? PaymentId { get; set; }

        public ChequeStatus Status { get; set; } = ChequeStatus.Draft;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? PrintedDate { get; set; }
        public DateTime? ClearedDate { get; set; }

        public string CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey("TemplateId")]
        public virtual ChequeTemplate Template { get; set; }

        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccount BankAccount { get; set; }
    }

    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountTitle { get; set; }

        public string BranchCode { get; set; }
        public string IBAN { get; set; }

        [DataType(DataType.Currency)]
        public decimal CurrentBalance { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Cheque> Cheques { get; set; }
    }

    public enum ChequeStatus
    {
        Draft,
        Approved,
        Printed,
        Issued,
        Cleared,
        Cancelled,
        Stopped
    }

    public class ChequePrintModel
    {
        public Cheque Cheque { get; set; }
        public ChequeTemplate Template { get; set; }
        public BankAccount BankAccount { get; set; }
        public string CurrentDate => DateTime.Now.ToString("dd MMMM, yyyy");
    }
}