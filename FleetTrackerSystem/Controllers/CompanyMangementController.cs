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
      

       
        public CompanyMangementController(
            IUnitOfWork unitOfWork)
            
            { 
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            IEnumerable<Company> companies = _unitOfWork.Company.GetAll();
            return Ok(companies);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
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
