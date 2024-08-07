using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.Controllers;
using TodoList.Api.Dtos;
using TodoList.Api.Services;
using Xunit;

namespace TodoList.Api.UnitTests.Controllers
{
    public class TodoItemsControllerTests
    {
        private readonly Mock<IService<TodoItemDto>> _serviceMock;
        private readonly Mock<ILogger<TodoItemsController>> _loggerMock;

        private readonly TodoItemsController _controller;

        public TodoItemsControllerTests()
        {
            _serviceMock = new Mock<IService<TodoItemDto>>();
            _loggerMock = new Mock<ILogger<TodoItemsController>>();
            _controller = new TodoItemsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_Should_Return_Incomplete_TodoItems()
        {
            // Arrange
            var todoItems = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Description = "Todo 1", IsCompleted = false },
                new TodoItemDto { Id = Guid.NewGuid(), Description = "Todo 2", IsCompleted = true },
                new TodoItemDto { Id = Guid.NewGuid(), Description = "Todo 3", IsCompleted = false }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(todoItems);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var incompleteResults = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Equal(2, incompleteResults.Count());
            Assert.All(incompleteResults, item => Assert.False(item.IsCompleted));
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_When_Description_Already_Exists()
        {
            // Arrange
            var todoItem = new TodoItemDto { Id = Guid.NewGuid(), Description = "Existing Todo", IsCompleted = false };

            _serviceMock.Setup(s => s.GetByContentAsync(todoItem.Description)).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.Post(todoItem);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Description already exists", badRequestResult.Value);
        }

        [Fact]
        public async Task Post_Should_Returns_CreatedAt_When_Is_New()
        {
            // Arrange
            var todoItem = new TodoItemDto { Description = "New Todo", IsCompleted = false };

            _serviceMock.Setup(s => s.GetByContentAsync(todoItem.Description)).ReturnsAsync((TodoItemDto)null);
            _serviceMock.Setup(s => s.AddAsync(todoItem)).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.Post(todoItem);

            // Assert
            _serviceMock.Verify(s => s.AddAsync(todoItem), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}