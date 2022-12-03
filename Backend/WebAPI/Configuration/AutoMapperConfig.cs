using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.Dto;
using Backend.WebAPI.DTO;
using Backend.WebAPI.Protos;

namespace Backend.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreationDTO, Category>();
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonCreationDTO, Person>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
