using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route("todo")]
    public class ToDoListController : ControllerBase
    {
        private IToDoService _toDoService;
        private IMapper _mapper;

        public ToDoListController(IToDoService toDoService, IMapper mapper)
        {
            _toDoService = toDoService;
            _mapper = mapper;
        }
        
    }
}