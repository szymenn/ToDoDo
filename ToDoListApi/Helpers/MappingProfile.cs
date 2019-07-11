using AutoMapper;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoDto, ToDo>();
            CreateMap<ToDo, ToDoDto>();
        }
    }
}