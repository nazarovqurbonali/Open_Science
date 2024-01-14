using System.Net;
using Domain.Dtos.DirectionDtos;
using Domain.Dtos.ScientificDirectionDtos;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ScientificDirectionService;

public class ScientificDirectionService : IScientificDirectionService
{
    private readonly ApplicationContext _context;

    public ScientificDirectionService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Response<List<GetDirectoryDto>>> GetDirections()
    {
        try
        {
            var scientificDirections = await (from sd in _context.Directions
                select new GetDirectoryDto()
                {
                    Id = sd.Id,
                    Name = sd.Name
                }).ToListAsync();
            return new Response<List<GetDirectoryDto>>(scientificDirections);
        }
        catch (Exception e)
        {
            return new Response<List<GetDirectoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetDirectoryDto>> GetDirectionById(int id)
    {
        try
        {
            var scientificDirection = await (from sd in _context.Directions
                select new GetDirectoryDto()
                {
                    Id = sd.Id,
                    Name = sd.Name
                }).FirstOrDefaultAsync(w => w.Id == id);
            return new Response<GetDirectoryDto>(scientificDirection);
        }
        catch (Exception e)
        {
            return new Response<GetDirectoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddDirection(AddDirectoryDto scientificDirection)
    {
        try
        {
            var result =
                await _context.Directions.FirstOrDefaultAsync(x => x.Name == scientificDirection.Name);
            if (result == null)
            {
                var direction = new Direction()
                {
                    Name = scientificDirection.Name
                };
                await _context.Directions.AddAsync(direction);
                await _context.SaveChangesAsync();
                return new Response<int>(direction.Id);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "already exists");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> UpdateDirection(UpdateDirectoryDto scientificDirection)
    {
        try
        {
            var oldDirection = await _context.Directions.FindAsync(scientificDirection.Id);
            if (oldDirection == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Scientific direction not found!");
            var direction = new Direction()
            {
                Id = oldDirection.Id,
                Name = scientificDirection.Name
            };
            _context.Directions.Update(direction);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteDirection(int id)
    {
        try
        {
            var direction = await _context.Directions.FindAsync(id);
            if (direction == null)
                return new Response<bool>(HttpStatusCode.BadRequest, "Scientific direction not found!");
            _context.Directions.Remove(direction);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}