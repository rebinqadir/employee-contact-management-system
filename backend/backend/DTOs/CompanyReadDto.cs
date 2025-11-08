namespace backend.DTOs
{
    public class CompanyReadDto
    {
        public int ID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string? Industry { get; set; }
        public string? Website { get; set; }
    }
}
