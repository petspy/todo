using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Dtos;

namespace TodoList.Api.Controllers
{
    public interface IController<TDto> where TDto : IDto
    {
        Task<IActionResult> Get();
        Task<IActionResult> GetById(Guid id);
        Task<IActionResult> Post([FromBody] TDto body);
        Task<IActionResult> Put(Guid id, [FromBody] TDto body);
    }
}