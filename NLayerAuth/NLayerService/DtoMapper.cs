using AutoMapper;
using NLayerCore.DTOs;
using NLayerCore.Models;

namespace NLayerService
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}