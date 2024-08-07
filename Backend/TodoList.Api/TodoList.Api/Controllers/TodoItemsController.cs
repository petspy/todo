using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Dtos;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    public class TodoItemsController : BaseController<TodoItemDto>
    {

        public TodoItemsController(IService<TodoItemDto> service, ILogger<TodoItemsController> logger) : base(service, logger)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var results = await _service.GetAllAsync();

            var incompleteResults = results.Where(x => !x.IsCompleted);

            return Ok(incompleteResults);
        }

        [HttpPost]
        public override async Task<IActionResult> Post(TodoItemDto todoItem)
        {
            var result = await _service.GetByContentAsync(todoItem.Description);

            if (result != null)
            {
                return BadRequest("Description already exists");
            }

            var response = await base.Post(todoItem);

            return response;
        }
    }
}
