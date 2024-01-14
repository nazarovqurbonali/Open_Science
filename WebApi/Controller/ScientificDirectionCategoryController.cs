using System.Net;
using Domain.Dtos.LocationDtos;
using Domain.Dtos.CategoryDtos;
using Domain.Responses;
using Infrastructure.Services.ScientificDirectionCategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class CategoryController : BaseController
{
    private readonly IScientificDirectionCategoryService _service;

    public CategoryController(IScientificDirectionCategoryService service)
    {
        _service = service;
    }
    
    [HttpGet("getcategories")]
    public async Task<IActionResult> GeScienceProjects()
    {
        var result = await _service.GetCategories();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("getcategory-by-id")]
    public async Task<IActionResult> GetScienceProjectById(int id)
    {
        var result = await _service.GetCategoryById(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("addcategory")]
    public async Task<IActionResult> AddScienceProject([FromForm] AddCategoryDto Category)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.AddCategory(Category);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("updatecategory")]
    public async Task<IActionResult> UpdateScienceProject([FromForm] UpdateCategoryDto Category)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.UpdateCategory(Category);
            return StatusCode(result.StatusCode, result);
        }

        var errors = ModelState.SelectMany(e => e.Value!.Errors.Select(er => er.ErrorMessage)).ToList();
        var response = new Response<LocationDto>(HttpStatusCode.BadRequest, errors);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("deletecategory")]
    public async Task<IActionResult> DeleteScienceProject(int id)
    {
        var result = await _service.DeleteCategory(id);
        return StatusCode(result.StatusCode, result);
    }
}