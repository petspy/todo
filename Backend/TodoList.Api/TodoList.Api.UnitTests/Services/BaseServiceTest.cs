using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TodoList.Api.Dtos;
using TodoList.Api.Models;
using TodoList.Api.Repositories;
using Xunit;
using TodoList.Api.Services;

namespace TodoList.Api.UnitTests.Services
{
    public class BaseServiceTests
    {
        private readonly Mock<IRepository<TodoItem>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BaseService<TodoItemDto, TodoItem> _baseService;

        public BaseServiceTests()
        {
            _repositoryMock = new Mock<IRepository<TodoItem>>();
            _mapperMock = new Mock<IMapper>();
            _baseService = new BaseService<TodoItemDto, TodoItem>(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Model_To_Repository_And_Return_Dto()
        {
            // Arrange
            var dto = new TodoItemDto { Id = Guid.NewGuid(), Description = "Test Todo Item" };
            var model = new TodoItem();

            _mapperMock.Setup(m => m.Map<TodoItem>(dto)).Returns(model);
            _repositoryMock.Setup(r => r.AddAsync(model)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<TodoItemDto>(model)).Returns(dto);

            // Act
            var result = await _baseService.AddAsync(dto);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(model), Times.Once);
            _mapperMock.Verify(m => m.Map<TodoItemDto>(model), Times.Once);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_DeleteAsync_On_Repository()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _baseService.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Dtos()
        {
            // Arrange
            var models = new List<TodoItem> { new TodoItem(), new TodoItem() };
            var dtos = new List<TodoItemDto> { new TodoItemDto(), new TodoItemDto() };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(models);
            _mapperMock.Setup(m => m.Map<TodoItemDto>(It.IsAny<TodoItem>())).Returns<TodoItem>(m => dtos[models.IndexOf(m)]);

            // Act
            var result = await _baseService.GetAllAsync();

            // Assert
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            Assert.Equal(dtos, result);
        }

        [Fact]
        public async Task GetByContentAsync_Should_Return_Dto_By_Content()
        {
            // Arrange
            var content = "Test Content";
            var model = new TodoItem();
            var dto = new TodoItemDto();

            _repositoryMock.Setup(r => r.GetByContentAsync(content)).ReturnsAsync(model);
            _mapperMock.Setup(m => m.Map<TodoItemDto>(model)).Returns(dto);

            // Act
            var result = await _baseService.GetByContentAsync(content);

            // Assert
            _repositoryMock.Verify(r => r.GetByContentAsync(content), Times.Once);
            _mapperMock.Verify(m => m.Map<TodoItemDto>(model), Times.Once);
            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Dto_By_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = new TodoItem();
            var dto = new TodoItemDto();

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(model);
            _mapperMock.Setup(m => m.Map<TodoItemDto>(model)).Returns(dto);

            // Act
            var result = await _baseService.GetByIdAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(m => m.Map<TodoItemDto>(model), Times.Once);
            Assert.Equal(dto, result);
        }

        [Fact]
        public void IsExistsAsync_Should_Return_Repository_Result()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedResult = true;

            _repositoryMock.Setup(r => r.IsExistsAsync(id)).Returns(expectedResult);

            // Act
            var result = _baseService.IsExistsAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.IsExistsAsync(id), Times.Once);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_UpdateAsync_On_Repository()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new TodoItemDto();

            // Act
            await _baseService.UpdateAsync(id, dto);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TodoItem>()), Times.Once);
        }
    }
}