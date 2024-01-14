using System.Net;
using AutoMapper;
using Domain.Dtos.ScienceProjectDtos;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.DataContext;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ScienceProjectService;

public class ScienceProjectService : IScienceProjectService
{
    private readonly ApplicationContext _context;
    private readonly IFileService _fileService;

    public ScienceProjectService(ApplicationContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Response<List<GetScienceProjectDto>>> GetScienceProjects(GetScienceProjectFilter filter)
    {
        try
        {
            var project = _context.ScienceProjects.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                project = project.Where(l =>
                    l.Name.ToLower().Contains(filter.Name.ToLower()));
            if (filter.DirectionId != 0)
            {
                project = project.Where(x => x.Id == filter.DirectionId);
            }


            var mapped = await (from p in project
                join d in _context.ScientificDirections on p.ScientificDirectionId equals d.Id
                select new GetScienceProjectDto()
                {
                    UserId = p.UserProfileId,
                    Id = p.Id,
                    FullName = string.Concat(p.UserProfile.FirstName + " " + p.UserProfile.LastName),
                    Name = p.Name,
                    ProjectFileName = p.ProjectFileName,
                    
                    DateUpdated = p.DateUpdated,
                    DateCreated = p.DateCreated,
                    Category = d.ScientificDirectionCategories.Select(x => x.CategoryName).ToList(),
                    ScientificDirectionName = d.Name
                    
                }).OrderByDescending(x=>x.DateUpdated).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).AsNoTracking().ToListAsync();

            var totalRecord = await project.CountAsync();
            return new PagedResponse<List<GetScienceProjectDto>>(mapped, filter.PageNumber, filter.PageSize,
                totalRecord);
        }
        catch (Exception e)
        {
            return new Response<List<GetScienceProjectDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetScienceProjectDto>> GetScienceProjectById(int id)
    {
        try
        {
            var mapped = await (from p in _context.ScienceProjects
                join d in _context.ScientificDirections on p.ScientificDirectionId equals d.Id
                select new GetScienceProjectDto()
                {
                    UserId = p.UserProfileId,
                    FullName = string.Concat(p.UserProfile.FirstName + p.UserProfile.LastName),
                    Id = p.Id,
                    Name = p.Name,
                    ProjectFileName = p.ProjectFileName,
                    DateUpdated = p.DateUpdated,
                    DateCreated = p.DateCreated,
                    Category = d.ScientificDirectionCategories.Select(x => x.CategoryName).ToList(),
                    ScientificDirectionName = d.Name
                }).FirstOrDefaultAsync(x => x.Id == id);
            ;
            return new Response<GetScienceProjectDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetScienceProjectDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> AddScienceProject(AddScienceProjectDto? scienceProject, string userId)
    {
        try
        {
            if (scienceProject != null)
            {
                var directionRequest1 = await _context.Directions.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == scienceProject.ScientificDirectionId);


                if (directionRequest1 == null)
                {
                    return new Response<int>(HttpStatusCode.BadRequest, "Not found direction ");
                }

                var newDirection = new ScientificDirection()
                {
                    Name = directionRequest1.Name,
                };

                await _context.ScientificDirections.AddAsync(newDirection);
                await _context.SaveChangesAsync();

                foreach (var id in scienceProject.CategoryId)
                {
                    var categoryRequest1 = await _context.Categories.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.DirectionId == scienceProject.ScientificDirectionId && x.Id == id);
                    if (categoryRequest1 == null)
                    {
                        return new Response<int>(HttpStatusCode.BadRequest, "Not found category");
                    }

                    var category = new ScientificDirectionCategory()
                    {
                        CategoryName = categoryRequest1.CategoryName,
                        ScientificDirectionId = newDirection.Id,
                    };
                    await _context.ScientificDirectionCategories.AddAsync(category);
                    await _context.SaveChangesAsync();
                }

                var file = _fileService.CreateFile(scienceProject.ProjectFileName);
                var project = new ScienceProject()
                {
                    Name = scienceProject.Name,
                    ProjectFileName = file.Data,
                    DateUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow,
                    UserProfileId = userId,
                    ScientificDirectionId = newDirection.Id
                };

                await _context.ScienceProjects.AddAsync(project);
                await _context.SaveChangesAsync();
                return new Response<int>(project.Id);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "not saved project");
        }
        catch (Exception ex)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> UpdateScienceProject(UpdateScienceProjectDto scienceProject, string userId)
    {
        try
        {
            var project = await _context.ScienceProjects.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == scienceProject.Id);
            if (project == null)
            {
                return new Response<bool>(false, "update failed");
            }

            project.ProjectFileName = scienceProject.ProjectFileName.FileName;
            project.Id = scienceProject.Id;
            project.Name = scienceProject.Name;
            project.DateCreated = scienceProject.DateCreated;
            project.DateUpdated = DateTime.UtcNow;

            _context.ScienceProjects.Update(project);
            await _context.SaveChangesAsync();

            return new Response<bool>(true, "OK");
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteScienceProject(int id)
    {
        try
        {
            var project = await _context.ScienceProjects.FindAsync(id);
            if (project == null) return new Response<bool>(HttpStatusCode.BadRequest, "Science project not found!");
            var projectFile = await _context.ScienceProjects.FirstOrDefaultAsync(s => s.Id == id);
            _fileService.DeleteFile(projectFile!.ProjectFileName);
            _context.ScienceProjects.Remove(project);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}