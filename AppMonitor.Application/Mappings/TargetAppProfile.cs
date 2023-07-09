using AppMonitor.Application.Dtos;
using AppMonitor.Domain.Entities;

using AutoMapper;

namespace AppMonitor.Application.Mappings
{
    public class TargetAppProfile : Profile
    {
        public TargetAppProfile()
        {
            CreateMap<TargetApp, TargetAppDto>();
            CreateMap<TargetAppDto, TargetApp>();
        }
    }
}