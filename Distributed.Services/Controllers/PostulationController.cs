using Application.Dto.Postulation;
using Application.MainModule.Interface;
using Hangfire.MemoryStorage.Dto;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostulationController : CoreController
{
    private readonly IPostulationAppService _postulationAppService;

    public PostulationController(IPostulationAppService postulationAppService)
    {
        _postulationAppService = postulationAppService;
    }

    /// <summary>
    /// Permite obtener una postulación por id
    /// </summary>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<PostulationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int postulationId)
    {
        var result = await _postulationAppService.GetById(postulationId);
        return new OkObjectResult(new JsonResult<PostulationDto>(result));
    }


    /// <summary>
    /// Permite agregar una postulación
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddPostulationDto postulationDto)
    {
        var response = await _postulationAppService.Add(postulationDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }


    /// <summary>
    /// Permite actualizar una postulación por id
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] PostulationDto updatePostulationDto)
    {
        var response = await _postulationAppService.Update(updatePostulationDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }
}