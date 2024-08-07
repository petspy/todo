using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoList.Api.Dtos;

namespace TodoList.Api.Services
{
    /// <summary>
    /// The purpose of IService is to provide a layer of abstraction between the API and the database via transformation, while providing CRUD operations.
    /// This allows the separate of concerns between the API and the database. This is where we should apply business rules and validation.
    /// </summary>
    public interface IService<TDto> where TDto : IDto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
        Task<TDto> GetByContentAsync(string content);
        Task<TDto> AddAsync(TDto dto);
        Task UpdateAsync(Guid id, TDto dto);
        Task DeleteAsync(Guid id);
        bool IsExistsAsync(Guid id);
    }
}
