using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Database;
using TodoList.Api.Models;

namespace TodoList.Api.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly ApplicationDbContext _context;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<TodoItem>> GetAllAsync()
        {
            return _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            var result = await _context.TodoItems.FindAsync(id);

            return result ?? throw new KeyNotFoundException($"{nameof(TodoItem)} with id: {id} is not found");
        }

        public async Task AddAsync(TodoItem model)
        {
            await _context.TodoItems.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem model)
        {
            _context.Entry(model).State = EntityState.Modified;

            _context.TodoItems.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
        }

        public Task<TodoItem> GetByContentAsync(string content)
        {
            return _context.TodoItems
                   .FirstOrDefaultAsync(x => x.Description.ToLowerInvariant() == content.ToLowerInvariant());

        }

        public bool IsExistsAsync(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }
    }
}