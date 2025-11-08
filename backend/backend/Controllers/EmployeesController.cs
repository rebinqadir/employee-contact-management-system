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


        /// <summary>
        /// Retrieves a paginated list of employees with optional search.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">Number of employees to return per page (default is 5).</param>
        /// <param name="search">Optional search term to filter by employee name or email or phone or job title.</param>
        /// <returns>Returns a paginated list of employees.</returns>
        /// <response code="200">Employees returned successfully.</response>
        /// <response code="400">Invalid pagination values provided.</response>
        /// <response code="500">An unexpected error occurred.</response>
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

        /// <summary>
        /// Retrieves a single employee by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>Employee details if found.</returns>
        /// <response code="200">Employee returned successfully.</response>
        /// <response code="404">No employee exists with the specified ID.</response>
        /// <response code="500">An unexpected error occurred.</response>
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


        /// <summary>
        /// Creates a new employee record.
        /// </summary>
        /// <param name="createDto">The employee data required to create a new record.</param>
        /// <returns>The newly created employee.</returns>
        /// <response code="201">Employee created successfully.</response>
        /// <response code="400">Validation failed. Returned when request data is invalid.</response>
        /// <response code="500">An unexpected error occurred.</response>
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

        /// <summary>
        /// Updates an existing employee with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="updateDto">The updated employee details.</param>
        /// <returns>No content if the update is successful.</returns>
        /// <response code="204">Employee updated successfully.</response>
        /// <response code="400">Validation failed for the provided employee data.</response>
        /// <response code="404">Employee with the specified ID was not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
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


        /// <summary>
        /// Deletes an existing employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        /// <response code="204">Employee deleted successfully.</response>
        /// <response code="404">Employee with the specified ID was not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
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
