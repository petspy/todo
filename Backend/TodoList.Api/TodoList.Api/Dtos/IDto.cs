using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Api.Models;

namespace TodoList.Api.Dtos
{
    /// <summary>
    /// This is a marker interface for all DTOs. All Dtos should implement this interface to be used in the API via IService.
    /// </summary>
    public interface IDto
    {
        Guid Id { get; init; }
    }
}