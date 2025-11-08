using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(255, ErrorMessage = "Employee name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(255, ErrorMessage = "Email address cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Phone number format is invalid.")]
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters.")]
        public string? Phone { get; set; }

        [StringLength(255, ErrorMessage = "Job Title cannot exceed 255 characters.")]
        public string? JobTitle { get; set; }


        [Required(ErrorMessage = "Company ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Company ID must be a positive integer.")]
        public int CompanyID { get; set; }

    }
}
