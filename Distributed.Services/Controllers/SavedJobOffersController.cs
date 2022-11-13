using Application.Dto.JobOffer;
using Application.Dto.SavedJobOffer;
using Application.MainModule.Interface;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SavedJobOffersController : CoreController
{
    private readonly ISavedJobOffersAppService _savedJobOffersAppService;

    public SavedJobOffersController(ISavedJobOffersAppService savedJobOffersAppService)
    {
        _savedJobOffersAppService = savedJobOffersAppService;
    }


    /// <summary>
    /// Permite agregar una oferta de trabajos a la lista de favoritos del postulante
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddSavedJobOffersDto jobOfferDto)
    {
        var response = await _savedJobOffersAppService.Add(jobOfferDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }



    /// <summary>
    /// Permite editar una oferta de trabajo guardada
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] SavedJobOffersDto updateJobOfferDto)
    {
        var response = await _savedJobOffersAppService.Update(updateJobOfferDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }


    /// <summary>
    /// Permite obtener las ofertas de trabajo guardadas por un postulante
    /// </summary>
    [HttpGet(nameof(GetByPostulant))]
    [ProducesResponseType(typeof(JsonResult<List<SavedJobOffersDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPostulant(int id)
    {
        var response = await _savedJobOffersAppService.GetByPostulant(id);
        return new OkObjectResult(new JsonResult<List<SavedJobOffersDto>>(response));
    }
}