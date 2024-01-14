using System.Net;
using Domain.Dtos.CategoryDtos;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ScientificDirectionCategoryServices;

public class ScientificDirectionCategoryService : IScientificDirectionCategoryService
{
    private readonly ApplicationContext _context;

    public ScientificDirectionCategoryService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Response<List<GetCategoryDto>>> GetCategories()
    {
        try
        {
            var directionCategories = await (from sdc in _context.Categories
                select new GetCategoryDto()
                {
                    Id = sdc.Id,
                    DirectionId = sdc.DirectionId,
                    CategoryName = sdc.CategoryName
                }).ToListAsync();
            return new Response<List<GetCategoryDto>>(directionCategories);
        }
        catch (Exception e)
        {
            return new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCategoryDto>> GetCategoryById(int id)
    {
        try
        {
            var directionCategory = await (from sdc in _context.Categories
                select new GetCategoryDto()
                {
                    Id = sdc.Id,
                    DirectionId = sdc.DirectionId,
                    CategoryName = sdc.CategoryName
                }).FirstOrDefaultAsync(w => w.Id == id);
            return new Response<GetCategoryDto>(directionCategory);
        }
        catch (Exception e)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddCategory(
        AddCategoryDto addCategory)
    {
        try
        {
            var category =
                await _context.Categories.FirstOrDefaultAsync(x =>
                    x.CategoryName == addCategory.CategoryName);

            if (category==null)
            {
                var directionCategory = new Category()
                {
                    DirectionId = addCategory.DirectionId,
                    CategoryName = addCategory.CategoryName
                };
                await _context.Categories.AddAsync(directionCategory);
                await _context.SaveChangesAsync();
                return new Response<int>(directionCategory.Id);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "already exists");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> UpdateCategory(
        UpdateCategoryDto addCategory)
    {
        try
        {
            var oldDirectionCategory =
                await _context.Categories.FindAsync(addCategory.Id);
            if (oldDirectionCategory == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Scientific direction category not found!");
            var directionCategory = new Category()
            {
                Id = addCategory.Id,
                DirectionId = addCategory.Id,
                CategoryName = addCategory.CategoryName
            };
            _context.Categories.Update(directionCategory);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCategory(int id)
    {
        try
        {
            var directionCategory =
                await _context.Categories.FindAsync(id);
            if (directionCategory == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Scientific direction category not found!");
            _context.Categories.Remove(directionCategory);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}