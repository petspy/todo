#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Repositories
{
    /// <summary>
    /// The purpose of IRepository is to provide direct access via CRUD to database, while abstracting the data access logic
    /// </summary>
    public interface IRepository<TModel> where TModel : IModel
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(Guid id);
        Task<TModel> GetByContentAsync(string content);
        Task AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
        bool IsExistsAsync(Guid id);
    }
}