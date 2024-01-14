using System.Net;
using Domain.Dtos.LocationDtos;
using Domain.Dtos.DirectionDtos;
using Domain.Responses;
using Infrastructure.Services.ScientificDirectionService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class DirectionController : BaseController
{
    private readonly IScientificDirectionService _service;

    public DirectionController(IScientificDirectionService service)
    {
        _service = service;
    }
    
    [HttpGet("getdirections")]
    public async Task<IActionResult> GeScienceProjects()
    {
        var result = await _service.GetDirections();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("getdirection-by-id")]
    public async Task<IActionResult> GetScienceProjectById(int id)
    {
        var result = await _service.GetDirectionById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("adddirection")]
    public async Task<IActionResult> AddScienceProject([FromForm] AddDirectoryDto Direction)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.AddDirection(Direction);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("updatedirection")]
    public async Task<IActionResult> UpdateScienceProject([FromForm] UpdateDirectoryDto Direction)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateDirection(Direction);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("deletedirection")]
    public async Task<IActionResult> DeleteDirection(int id)
    {
        var result = await _service.DeleteDirection(id);
        return StatusCode(result.StatusCode, result);
    }
}