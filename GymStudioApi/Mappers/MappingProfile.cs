using AutoMapper;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;

namespace GymStudioApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassRequest, Class>();
        }
    }
}