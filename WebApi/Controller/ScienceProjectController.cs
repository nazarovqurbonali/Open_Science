using System.Net;
using Domain.Dtos.LocationDtos;
using Domain.Dtos.ScienceProjectDtos;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ScienceProjectService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[Authorize]
public class ScienceProjectController : BaseController
{
    private readonly IScienceProjectService _service;

    public ScienceProjectController(IScienceProjectService service)
    {
        _service = service;
    }

    [HttpGet("get-science-projects")]
    [AllowAnonymous]
    public async Task<IActionResult> GeScienceProjects(GetScienceProjectFilter filter)
    {
        var result = await _service.GetScienceProjects(filter);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-science-project-by-id")]
    [AllowAnonymous]
    public async Task<IActionResult> GetScienceProjectById(int id)
    {
        var result = await _service.GetScienceProjectById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-science-project")]
    public async Task<IActionResult> AddScienceProject([FromForm] AddScienceProjectDto scienceProject)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
            var result = await _service.AddScienceProject(scienceProject,userId);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-science-project")]
    public async Task<IActionResult> UpdateScienceProject([FromForm] UpdateScienceProjectDto scienceProject)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
            var result = await _service.UpdateScienceProject(scienceProject,userId);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-science-project")]
    public async Task<IActionResult> DeleteScienceProject(int id)
    {
        var result = await _service.DeleteScienceProject(id);
        return StatusCode(result.StatusCode, result);
    }
}