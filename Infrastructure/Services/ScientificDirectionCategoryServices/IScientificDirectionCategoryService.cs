using Domain.Dtos.CategoryDtos;
using Domain.Responses;

namespace Infrastructure.Services.ScientificDirectionCategoryServices;

public interface IScientificDirectionCategoryService
{
    Task<Response<List<GetCategoryDto>>> GetCategories();
    Task<Response<GetCategoryDto>> GetCategoryById(int id);
    Task<Response<int>> AddCategory(AddCategoryDto addCategory);
    Task<Response<bool>> UpdateCategory(
        UpdateCategoryDto addCategory);
    Task<Response<bool>> DeleteCategory(int id);
}