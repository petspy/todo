using System;
using AutoMapper;
using TodoList.Api.Dtos;
using TodoList.Api.Internal;
using TodoList.Api.Models;
using Xunit;

namespace TodoList.Api.UnitTests.Configurations
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfile_Configuration_IsValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingProfile_Maps_TodoItemDto_To_TodoItem()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = configuration.CreateMapper();
            var todoItemDto = new TodoItemDto { Id = Guid.NewGuid(), Description = "Test Todo Item" };

            // Act
            var todoItem = mapper.Map<TodoItem>(todoItemDto);

            // Assert
            Assert.Equal(todoItemDto.Id, todoItem.Id);
            Assert.Equal(todoItemDto.Description, todoItem.Description);
        }

        [Fact]
        public void MappingProfile_Maps_TodoItem_To_TodoItemDto()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = configuration.CreateMapper();
            var todoItem = new TodoItem { Id = Guid.NewGuid(), Description = "Test Todo Item" };

            // Act
            var todoItemDto = mapper.Map<TodoItemDto>(todoItem);

            // Assert
            Assert.Equal(todoItem.Id, todoItemDto.Id);
            Assert.Equal(todoItem.Description, todoItemDto.Description);
        }
    }
}