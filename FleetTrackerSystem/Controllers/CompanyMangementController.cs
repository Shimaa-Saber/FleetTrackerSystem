using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.Company;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FleetTrackerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyMangementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompanyMangementController> _logger;



        public CompanyMangementController(
            IUnitOfWork unitOfWork,
            ILogger<CompanyMangementController> logger
            )
            
            { 
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            IEnumerable<Company> companies = _unitOfWork.Company.GetAll();
            _logger.LogInformation("Retrieved {Count} companies from the database.", companies.Count());
            return Ok(companies);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided: {Id}", id);
                return BadRequest("Invalid ID provided.");
                
            }
            if (!_unitOfWork.Company.Exists(id))
            {
                _logger.LogWarning("Company with ID {Id} not found.", id);
                return NotFound("Company not found.");
            }   

            var company = _unitOfWork.Company.GetByID(id);
            return Ok(company);
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(AddCompany Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = Dto.Map<Company>();
            _logger.LogInformation("Adding new company with name: {CompanyName}", company.Name);

            _unitOfWork.Company.Add(company);
            await _unitOfWork.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.ID }, Dto); 
        }



        [HttpPut("{id}/UpdateCompany")]
        public IActionResult UpdateCompany(int id, UpdateCompanyDto Dto)
        {
            if (id <= 0 || id != Dto.Id)
            {
                return BadRequest("Invalid ID provided.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var company = _unitOfWork.Company.GetByID(id);
            if (company == null)
            {
                return NotFound("Company not found.");
            }
            var updatedCompany = Dto.Map<Company>();

            _unitOfWork.Company.Update(updatedCompany);
            _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}/RemoveCompany")]
        public async Task<IActionResult> DeleteCompany(int id) { 
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var company = _unitOfWork.Company.GetByID(id);
            if (company == null)
            {
                return NotFound("Company not found.");
            }
            _unitOfWork.Company.Remove(id);
          await  _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

    }
}
