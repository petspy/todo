using AutoMapper;
using TodoList.Api.Dtos;
using TodoList.Api.Models;

namespace TodoList.Api.Internal
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItemDto, TodoItem>().ReverseMap();
        }
    }
}