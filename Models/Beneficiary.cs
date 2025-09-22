using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string CNIC { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required]
        public BeneficiaryType Type { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        // Navigation properties
        public virtual ICollection<Payment> Payments { get; set; }

        public Beneficiary()
        {
            Payments = new HashSet<Payment>();
        }
    }

    public enum BeneficiaryType
    {
        Individual,
        SME,
        Farmer,
        NGO,
        GovernmentDepartment
    }
}