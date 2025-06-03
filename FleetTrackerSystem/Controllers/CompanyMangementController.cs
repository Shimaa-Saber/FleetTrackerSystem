using FleetTrackerSystem.CQRS.CompanyMangement.Comands;
using FleetTrackerSystem.CQRS.CompanyMangement.Queries;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.DTOS.Company;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using FleetTrackerSystem.ViewModels;
using MediatR;
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
        
        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _mediator.Send(new GetAllCompaniesQuery());
            _logger.LogInformation("Retrieved {Count} companies from the database.", companies.Count());
            return ResponseViewModel<IEnumerable<Company>>.Success(companies);
        }
        
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

        [HttpPost("AddCompany")]
        public async void AddCompany(AddCompany Dto)
        {

            _logger.LogInformation("Adding new company with name");

            var command = Dto.Map<AddCompanyComand>();
             await _mediator.Send(command);

        }



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
