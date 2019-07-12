using AutoMapper;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoBindingModel, ToDo>();
            CreateMap<ToDo, ToDoViewModel>();
        }
    }
}