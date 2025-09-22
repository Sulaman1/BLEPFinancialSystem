using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalBudget { get; set; }

        [DataType(DataType.Currency)]
        public decimal FundsDisbursed { get; set; }

        [DataType(DataType.Currency)]
        public decimal FundsUtilized { get; set; }

        public ProjectStatus Status { get; set; }

        // Navigation properties
        public virtual ICollection<BudgetCategory> BudgetCategories { get; set; }
        public virtual ICollection<Disbursement> Disbursements { get; set; }

        public Project()
        {
            BudgetCategories = new HashSet<BudgetCategory>();
            Disbursements = new HashSet<Disbursement>();
        }
    }

    public enum ProjectStatus
    {
        Planning,
        Active,
        Suspended,
        Completed,
        Closed
    }
}