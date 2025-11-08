using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? search = null)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest(new { message = "Page number and page size must be greater than zero." });

            try
            {
                // Deconstruct the tuple returned by the service
                //var (employees, totalCount) = await _employeeService.GetEmployeesAsync(pageNumber,pageSize,search);
                var (employees, totalCount) = await _employeeService.GetEmployeesAsync(pageNumber, pageSize, search);

                // Add the total count to a custom header for client-side pagination UI
                Response.Headers.Append("X-Total-Count", totalCount.ToString());

                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {

            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound(new { message = "Employee not found" });

                return Ok(employee);

            }

            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdEmployee = await _employeeService.AddEmployeeAsync(createDto);
                // Returns HTTP 201
                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.ID }, createdEmployee);
            }

            catch (ArgumentException ex)
            {
                // Handle business logic validation errors
                return BadRequest(new { message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(id, updateDto);

                if (!result)
                {
                    // This typically means the employee with the given ID was not found.
                    return NotFound(new { message = "Employee not found" });
                }

                // Returns HTTP 204 No Content, which is standard for a successful PUT update
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                // Handle business logic validation errors
                return BadRequest(new { message = ex.Message });
            }

            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {

            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result)
                    return NotFound(new { message = "Employee not found" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }


    }
}
