using DapperExample.Models;
using DapperExample.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service=service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.GetCompanies().ConfigureAwait(false);
            return Ok(companies);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _service.GetCompanyById(id).ConfigureAwait(false);
            return Ok(company);
        }

        [HttpGet("buscar/{id:int}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdWithParameters(int id, string name)
        {
            var company = await _service.GetCompanyByIdWithParameters(id, name).ConfigureAwait(false);
            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Company company)
        {
            var id = await _service.CreateCompany(company).ConfigureAwait(false);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Company company)
        {
            await _service.UpdateComapny(company).ConfigureAwait(false);
            return Ok();
        }
    }
}
