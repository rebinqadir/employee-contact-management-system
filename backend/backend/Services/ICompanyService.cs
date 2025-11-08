using backend.DTOs;

namespace backend.Services
{
    public interface ICompanyService
    {
        Task<List<CompanyReadDto>> GetAllCompaniesAsync();
    }
}
