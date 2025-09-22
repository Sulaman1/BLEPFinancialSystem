using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }
    }

    public static class UserRoles
    {
        public const string WorldBank = "WorldBank";
        public const string ProjectDirector = "ProjectDirector";
        public const string FinanceOfficer = "FinanceOfficer";
        public const string MEOfficer = "MEOfficer";
        public const string ImplementingPartner = "ImplementingPartner";
        public const string Beneficiary = "Beneficiary";
    }
}