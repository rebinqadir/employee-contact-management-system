using backend.DTOs;

namespace backend.Services
{
    public interface IEmployeeService
    {
        Task<(List<EmployeeReadDto> items, int totalCount)> GetEmployeesAsync(int pageNumber, int pageSize, string? search);
        Task<EmployeeReadDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
