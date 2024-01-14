using AutoMapper;
using Domain.Dtos.LocationDtos;
using Domain.Dtos.ScienceProjectDtos;
using Domain.Dtos.UserDtos;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AddLocationDto, Location>();
        CreateMap<UpdateLocationDto, Location>();
        CreateMap<Location, GetLocationDto>();

        CreateMap<ScienceProject, AddScienceProjectDto>().ReverseMap();
        CreateMap<ScienceProject, GetScienceProjectDto>().ReverseMap();
        CreateMap<ScienceProject, UpdateScienceProjectDto>().ReverseMap();
        
        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.DateRegistered, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}