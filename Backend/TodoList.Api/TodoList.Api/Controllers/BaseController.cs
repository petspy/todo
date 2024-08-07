using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.Api.Dtos;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    /// <summary>
    /// This might be overkill, the basecontroller class would handles all CRUD operation using injected DTO typed service
    /// It also allows the derived controller to override the CRUD operation for further customisation
    /// Assuming deletion is not exposed to the API, therefore is not being implemented here
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TDto> : ControllerBase, IController<TDto> where TDto : IDto
    {
        protected readonly IService<TDto> _service;
        protected readonly ILogger<BaseController<TDto>> _logger;

        public BaseController(IService<TDto> service, ILogger<BaseController<TDto>> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var results = await _service.GetAllAsync();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TDto body)
        {
            var result = await _service.AddAsync(body);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody] TDto body)
        {
            if (id != body.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(id, body);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.IsExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}