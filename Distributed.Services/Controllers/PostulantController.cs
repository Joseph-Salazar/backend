﻿using Application.Dto.Company;
using Application.Dto.Postulant;
using Application.MainModule;
using Application.MainModule.Interface;
using Domain.MainModule.Entity;
using Hangfire.MemoryStorage.Dto;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostulantController : CoreController
{
    private readonly IPostulantAppService _postulantAppService;

    public PostulantController(IPostulantAppService postulantAppService)
    {
        _postulantAppService = postulantAppService;
    }

    /// <summary>
    /// Permite registrar un postulante
    /// </summary>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(JsonResult<PostulantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterPostulant(RegisterPostulantDto register)
    {
        var result = await _postulantAppService.Register(register);
        return new OkObjectResult(new JsonResult<PostulantDto>(result));
    }

    /// <summary>
    /// Permite a un postulante ingresar al sistema
    /// </summary>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(JsonResult<LoginPostulantResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginPostulant(LoginPostulantDto loginCompany)
    {
        var result = await _postulantAppService.Login(loginCompany);
        return new OkObjectResult(new JsonResult<LoginPostulantResponseDto>(result));
    }

    /// <summary>
    /// Permite obtener un postulante por id
    /// </summary>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<PostulantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int postulantId)
    {
        var result = await _postulantAppService.GetById(postulantId);
        return new OkObjectResult(new JsonResult<PostulantDto>(result));
    }

    /// <summary>
    /// Permite agregar un postulante
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddPostulantDto postulantDto)
    {
        var response = await _postulantAppService.Add(postulantDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite actualizar un postulante
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdatePostulantDto updatePostulantDto)
    {
        var response = await _postulantAppService.Update(updatePostulantDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }
}