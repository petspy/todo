using AutoMapper;
using TodoList.Api.Dtos;
using TodoList.Api.Models;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services
{
    public class TodoItemService : BaseService<TodoItemDto, TodoItem>
    {
        public TodoItemService(IRepository<TodoItem> repository, IMapper mapper) : base(repository, mapper) { }

        // Uncomment to override the base service method
        // public override Task<IEnumerable<TodoItemDto>> GetAllAsync()
        // {
        //     return base.GetAllAsync();
        // }
    }
}