using FleetTrackerSystem.Application.CQRS.CompanyMangement.Comands;
using FleetTrackerSystem.Application.CQRS.CompanyMangement.Queries;
using FleetTrackerSystem.Application.DTOS.Company;
using FleetTrackerSystem.Application.ViewModels;

using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Runtime.CompilerServices;

using System.Threading.Tasks;

namespace FleetTrackerSystem.API.Controllers
{
    [EnableRateLimiting("FixedPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyMangementController : ControllerBase
    {
      
        private readonly ILogger<CompanyMangementController> _logger;
        private readonly IMediator _mediator;



        public CompanyMangementController(
       
            ILogger<CompanyMangementController> logger,
            IMediator mediator
            )
            
            { 
          
            _logger = logger;
            _mediator = mediator;
        }
        [Authorize(Roles = "SuperAdmin")]

        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _mediator.Send(new GetAllCompaniesQuery());
            _logger.LogInformation("Retrieved {Count} companies from the database.", companies.Count());
            return ResponseViewModel<IEnumerable<Company>>.Success(companies);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<ResponseViewModel<Company>> GetCompanyById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided: {Id}", id);
             
                
            }

            var company = await _mediator.Send(new GetCompanyByIdQuery (id)  );
           return ResponseViewModel<Company>.Success(company); 
           
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("AddCompany")]
        public async void AddCompany(AddCompany Dto)
        {
            _logger.LogInformation("Adding new company with name");

            var command = Dto.Map<AddCompanyComand>();
            await _mediator.Send(command);
        }
        [Authorize(Roles = "SuperAdmin")]

        [HttpPut("{id}/UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyDto Dto)
        {
            if (id <= 0 || id != Dto.Id)
            {
                return BadRequest("Invalid ID provided.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comand = Dto.Map<UpdateCompanyComand>();
            await _mediator.Send(comand);
           
            return NoContent();
        }
        [Authorize(Roles = "SuperAdmin")]

        [HttpDelete("{id}/RemoveCompany")]
        public async Task<IActionResult> DeleteCompany(int id) { 
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
          await _mediator.Send(new RemoveCompanyComand { id=id});
            
            return NoContent();
        }

    }
}
