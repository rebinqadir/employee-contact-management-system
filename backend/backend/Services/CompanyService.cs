using backend.Data;
using backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;

        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyReadDto>> GetAllCompaniesAsync()
        {
            var companies = await _context.Companies
                .OrderBy(c => c.ID)
                .ToListAsync();

            // Manual DTO Mapping
            return companies.Select(c => new CompanyReadDto
            {
                ID = c.ID,
                CompanyName = c.CompanyName,
                Domain = c.Domain,
                Industry = c.Industry,
                Website = c.Website
            }).ToList();
        }
    }
}
