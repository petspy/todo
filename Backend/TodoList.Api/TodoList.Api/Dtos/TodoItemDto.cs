using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Dtos
{
    public record TodoItemDto : IDto
    {
        // This optional id can be different from the id in the database
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; init; }
        public bool IsCompleted { get; init; }
    };
}