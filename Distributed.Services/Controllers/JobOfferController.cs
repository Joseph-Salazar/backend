using Application.Dto.JobOffer;
using Application.MainModule.Interface;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobOfferController : CoreController
{
    private readonly IJobOfferService _jobOfferService;

    public JobOfferController(IJobOfferService jobOfferService)
    {
        _jobOfferService = jobOfferService;
    }

    /// <summary>
    /// Permite obtener una oferta de trabajo por id
    /// </summary>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<JobOfferDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int jobOfferId)
    {
        var result = await _jobOfferService.GetById(jobOfferId);
        return new OkObjectResult(new JsonResult<JobOfferDto>(result));
    }

    /// <summary>
    /// Permite obtener todas las ofertas de trabajo
    /// </summary>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(JsonResult<List<JobOfferDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _jobOfferService.GetAll();
        return new OkObjectResult(new JsonResult<List<JobOfferDto>>(result));
    }

    /// <summary>
    /// Permite obtener todas las ofertas de trabajo de una compañía
    /// </summary>
    [HttpGet("GetByCompany")]
    [ProducesResponseType(typeof(JsonResult<List<JobOfferDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByCompany(int id)
    {
        var result = await _jobOfferService.GetByCompany(id);
        return new OkObjectResult(new JsonResult<List<JobOfferDto>>(result));
    }


    /// <summary>
    /// Permite agregar una oferta de trabajo
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddJobOfferDto jobOfferDto)
    {
        var response = await _jobOfferService.Add(jobOfferDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite actualizar una compañía
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] JobOfferDto updateJobOfferDto)
    {
        var response = await _jobOfferService.Update(updateJobOfferDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }
}