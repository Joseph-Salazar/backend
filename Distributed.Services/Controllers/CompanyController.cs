using Application.Dto.Company;
using Application.Dto.Postulant;
using Application.MainModule;
using Application.MainModule.Interface;
using Hangfire.MemoryStorage.Dto;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : CoreController
{
    private readonly ICompanyAppService _companyAppService;

    public CompanyController(ICompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }

    /// <summary>
    /// Permite registrar una compañía
    /// </summary>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(JsonResult<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCompany(RegisterCompanyDto register)
    {
        var result = await _companyAppService.Register(register);
        return new OkObjectResult(new JsonResult<CompanyDto>(result));
    }


    /// <summary>
    /// Permite a una compañía ingresar al sistema
    /// </summary>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(JsonResult<LoginCompanyResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogingCompany(LoginCompanyDto loginCompany)
    {
        var result = await _companyAppService.Login(loginCompany);
        return new OkObjectResult(new JsonResult<LoginCompanyResponseDto>(result));
    }

    /// <summary>
    /// Permite obtener una compañía por id
    /// </summary>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<CompanyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int companyId)
    {
        var result = await _companyAppService.GetById(companyId);
        return new OkObjectResult(new JsonResult<CompanyDto>(result));
    }

    /// <summary>
    /// Permite agregar una compañía
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCompanyDto companyDto)
    {
        var response = await _companyAppService.Add(companyDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite actualizar una compañía
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCompanyDto updateCompanyDto)
    {
        var response = await _companyAppService.Update(updateCompanyDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }
}