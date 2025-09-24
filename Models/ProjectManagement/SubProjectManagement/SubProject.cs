using System.ComponentModel.DataAnnotations;

namespace BLEPFinancialSystem.Models.ProjectManagement.SubProjectManagement
{
    public class SubProject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ApprovedBudget { get; set; }

        public SubProjectStatus Status { get; set; } = SubProjectStatus.Proposed;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<SubProjectComponent> Components { get; set; } = new HashSet<SubProjectComponent>();
    }
}
