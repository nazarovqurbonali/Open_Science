using Domain.Dtos.ScienceProjectDtos;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ScienceProjectService;
public interface IScienceProjectService
{
    Task<Response<List<GetScienceProjectDto>>> GetScienceProjects(GetScienceProjectFilter filter);
    Task<Response<GetScienceProjectDto>> GetScienceProjectById(int id);
    Task<Response<int>> AddScienceProject(AddScienceProjectDto scienceProject,string userId);
    Task<Response<bool>> UpdateScienceProject(UpdateScienceProjectDto scienceProject,string userId);
    Task<Response<bool>> DeleteScienceProject(int id);
}