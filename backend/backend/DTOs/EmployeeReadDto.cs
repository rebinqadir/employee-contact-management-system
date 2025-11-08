namespace backend.DTOs
{
    public class EmployeeReadDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? JobTitle { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }


        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
    }
}
