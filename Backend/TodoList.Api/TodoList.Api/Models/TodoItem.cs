using System;

namespace TodoList.Api.Models
{
    public class TodoItem : IModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
