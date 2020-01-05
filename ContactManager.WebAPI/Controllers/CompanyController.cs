using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Services.Interfaces;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _companyService;

        public CompanyController(ICompanyServices companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Company>>> GetCompanies()
        {
            return Ok(await _companyService.ListCompanies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            return Ok(await _companyService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            return Ok(await _companyService.UpdateCompany(id, company));
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {

            var response = await _companyService.AddCompany(company);
            return CreatedAtAction("GetCompany", new { id = company.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(Guid id)
        {
            var company = await _companyService.RemoveCompany(id);
            return company;
        }

        [HttpPut("/Address/{id}")]
        public async Task<ActionResult<Address>> UpdateCompanyAddress(Guid id, Address address)
        {
            return Ok(await _companyService.UpdateCompanyAddress(id, address));
        }

        [HttpPost("/AddCompanyAddress/{id}")]
        public async Task<ActionResult<Company>> AddCompanyAddress(Guid id, Address address)
        {
            Company response = await _companyService.AddCompanyAddress(id, address);

            return CreatedAtAction("GetCompany", new { id = id }, response);
        }
    }
}
