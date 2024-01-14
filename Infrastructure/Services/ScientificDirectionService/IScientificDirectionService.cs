using Domain.Dtos.DirectionDtos;
using Domain.Responses;

namespace Infrastructure.Services.ScientificDirectionService;

public interface IScientificDirectionService
{
    Task<Response<List<GetDirectoryDto>>> GetDirections();
    Task<Response<GetDirectoryDto>> GetDirectionById(int id);
    Task<Response<int>> AddDirection(AddDirectoryDto scientificDirection);
    Task<Response<bool>> UpdateDirection(UpdateDirectoryDto scientificDirection);
    Task<Response<bool>> DeleteDirection(int id);
}