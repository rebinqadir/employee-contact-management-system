using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(255)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Domain { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Industry { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }

        // Navigation property
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
