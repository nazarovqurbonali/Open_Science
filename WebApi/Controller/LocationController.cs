using System.Net;
using Domain.Dtos.LocationDtos;
using Domain.Filters.LocationFilter;
using Domain.Responses;
using Infrastructure.Services.LocationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class LocationController : BaseController
{
    private readonly ILocationService _service;

    public LocationController(ILocationService service)
    {
        _service = service;
    }

    [HttpGet("get-locations")]
    public async Task<IActionResult> GetLocations([FromQuery] LocationFilter filter)
    {
        var result = await _service.GetLocations(filter);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get-location-by-id")]
    public async Task<IActionResult> GetLocationById(int id)
    {
        var result = await _service.GetLocationById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("add-location")]
    public async Task<IActionResult> AddLocation([FromBody] AddLocationDto location)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.AddLocation(location);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-location")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationDto location)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateLocation(location);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-location")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var result = await _service.DeleteLocation(id);
        return StatusCode(result.StatusCode, result);
    }
}