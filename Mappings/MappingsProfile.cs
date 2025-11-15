

using ASP.NET_Core_REST_API.DTOs;
using ASP.NET_Core_REST_API.Models;
using AutoMapper;

namespace ASP.NET_Core_REST_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskDTO>();
            
            CreateMap<CreateTaskDTO, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false));

            CreateMap<UpdateTaskDTO, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedAt, opt => opt.Ignore());
        }

    }
}