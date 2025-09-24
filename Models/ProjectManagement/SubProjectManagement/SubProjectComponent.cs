using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLEPFinancialSystem.Models.ProjectManagement.SubProjectManagement
{
    public class SubProjectComponent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        // Navigation property
        [ForeignKey("SubProjectId")]
        public virtual SubProject SubProject { get; set; }
    }
}
