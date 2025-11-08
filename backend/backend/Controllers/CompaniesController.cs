using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        /// <summary>
        /// Retrieves all companies
        /// </summary>
        /// <returns>Returns a list of companies.</returns>
        /// <response code="200">Companies returned successfully.</response>
        /// <response code="400">No companies were found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync();

                if (companies == null)
                    return NotFound(new { Message = "Companies not found" });

                return Ok(companies);
            }

            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }

        }

    }
}
