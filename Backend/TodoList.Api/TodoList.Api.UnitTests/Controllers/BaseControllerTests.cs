using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoList.Api.Controllers;
using TodoList.Api.Dtos;
using TodoList.Api.Services;
using Xunit;

namespace TodoList.Api.UnitTests.Controllers
{
    public class BaseControllerTests
    {
        private readonly Mock<IService<TestDto>> _serviceMock;
        private readonly Mock<ILogger<BaseController<TestDto>>> _loggerMock;
        private readonly BaseController<TestDto> _controller;

        public BaseControllerTests()
        {
            _serviceMock = new Mock<IService<TestDto>>();
            _loggerMock = new Mock<ILogger<BaseController<TestDto>>>();
            _controller = new BaseController<TestDto>(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResultWithResults()
        {
            // Arrange
            var testDtos = new List<TestDto> { new TestDto { Id = Guid.NewGuid(), Name = "Test" } };
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(testDtos);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResults = Assert.IsAssignableFrom<IEnumerable<TestDto>>(okResult.Value);
            Assert.Equal(testDtos, actualResults);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResultWithResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var testDto = new TestDto { Id = id, Name = "Test" };
            _serviceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(testDto);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<TestDto>(okResult.Value);
            Assert.Equal(testDto, actualResult);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((TestDto)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_WithValidDto_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var testDto = new TestDto { Id = Guid.NewGuid(), Name = "Test" };
            _serviceMock.Setup(x => x.AddAsync(testDto)).ReturnsAsync(testDto);

            // Act
            var result = await _controller.Post(testDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(BaseController<TestDto>.GetById), createdAtActionResult.ActionName);
            Assert.Equal(testDto.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(testDto, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Put_WithMatchingIdAndDto_ReturnsNoContentResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var testDto = new TestDto { Id = id, Name = "Test" };

            // Act
            var result = await _controller.Put(id, testDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Put_WithMismatchingIdAndDto_ReturnsBadRequestResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var testDto = new TestDto { Id = Guid.NewGuid(), Name = "Test" };

            // Act
            var result = await _controller.Put(id, testDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var testDto = new TestDto { Id = id, Name = "Test" };
            _serviceMock.Setup(x => x.UpdateAsync(id, testDto)).ThrowsAsync(new DbUpdateConcurrencyException());
            _serviceMock.Setup(x => x.IsExistsAsync(id)).Returns(false);

            // Act
            var result = await _controller.Put(id, testDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

    public class TestDto : IDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
    }
}