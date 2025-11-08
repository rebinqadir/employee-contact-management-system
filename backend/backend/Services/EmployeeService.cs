using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        public EmployeeService(DataContext context)
        {
            _context = context;
        }


        public async Task<(List<EmployeeReadDto> items, int totalCount)> GetEmployeesAsync(int pageNumber, int pageSize, string? search)
        {
            // 1. Initialize the query with eager loading
            var query = _context.Employees
                                .Include(e => e.Company)
                                .AsNoTracking() // For performance
                                .AsQueryable();

            // 2. Apply Search Filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(e =>
                    e.Name.ToLower().Contains(search) ||
                    e.Email.ToLower().Contains(search) ||
                    e.JobTitle != null && e.JobTitle.ToLower().Contains(search) ||
                    e.Phone != null && e.Phone.Contains(search));
            }

            // 3. Calculate Total Count
            int totalCount = await query.CountAsync();

            // 4. Apply Default Ordering and Pagination
            int skipCount = (pageNumber - 1) * pageSize;

            var employees = await query
                .OrderBy(e => e.ID) // Crucial for stable pagination
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            // 5. Manual DTO Mapping
            var employeeDtos = employees.Select(e => new EmployeeReadDto
            {
                ID = e.ID,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                JobTitle = e.JobTitle,
                CompanyID = e.CompanyID,
                CompanyName = e.Company?.CompanyName,
                IsActive = e.IsActive,
                CreatedAt = e.CreatedAt
            }).ToList();

            // 6. Return the tuple and totalcount
            return (employeeDtos, totalCount);
        }



        public async Task<EmployeeReadDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.ID == id);

            if (employee == null)
                return null;

            return new EmployeeReadDto
            {
                ID = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                JobTitle = employee.JobTitle,
                CompanyID = employee.CompanyID,
                CompanyName = employee.Company?.CompanyName,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt
            };
        }
        public async Task<EmployeeReadDto> AddEmployeeAsync(EmployeeCreateDto dto)
        {
            // Validate company exists
            var company = await _context.Companies.FindAsync(dto.CompanyID);

            if (company == null)
            {
                throw new ArgumentException("Company not found");
            }

            // Check if email already exists
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == dto.Email);
            if (existingEmployee != null)
            {
                throw new ArgumentException("Employee with this email already exists");
            }

            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                JobTitle = dto.JobTitle,
                CompanyID = dto.CompanyID,
                IsActive = true, // Default
                CreatedAt = DateTime.UtcNow // Default
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Reload with company data
            await _context.Entry(employee).Reference(e => e.Company).LoadAsync();

            // Manual mapping for return DTO
            return new EmployeeReadDto
            {
                ID = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                JobTitle = employee.JobTitle,
                CompanyID = employee.CompanyID,
                CompanyName = company?.CompanyName,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt
            };
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
        {

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return false; // Employee not found

            // Check if email already exists (excluding current employee)
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == dto.Email && e.ID != id);
            if (existingEmployee != null)
            {
                throw new ArgumentException("Employee with this email already exists");
            }

            // Validate company exists
            var company = await _context.Companies.FindAsync(dto.CompanyID);
            if (company == null)
            {
                throw new ArgumentException("Company not found");
            }

            // Manual mapping
            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.JobTitle = dto.JobTitle;
            employee.CompanyID = dto.CompanyID;
            employee.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
