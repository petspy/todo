using System;
using TodoList.Api.Dtos;

namespace TodoList.Api.Models
{
    /// <summary>
    /// This is a marker interface for all models. All models should implement this interface to be used for data access layer via IRepository.
    /// </summary>
    public interface IModel
    {
        Guid Id { get; set; }
    }
}