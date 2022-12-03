using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.Controllers;
using Backend.WebAPI.DataAccess.Repositories;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Backend.WebAPI.Dto;
using Backend.WebAPI.DTO;

namespace Backend.UnitTests
{
    public class CategoryControllerTests
    {
        private CategoryController _controller;
        public Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private CategoryMock _categoryMock;

        public CategoryControllerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _categoryMock = new CategoryMock();
        }

        [Fact]
        async public Task GetAllMethod_Returns_Ok()
        {
            var categoriesList = _categoryMock.GetAll();
            _categoryRepositoryMock.Setup(rep => rep.GetAllAsync()).Returns(Task.FromResult(categoriesList));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.GetAll();

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(categoriesList);
        }

        [Fact]
        async public Task GetCategory_Returns_Ok()
        {
            var id = 1;
            var category = _categoryMock.GetCategoryById(id);
            _categoryRepositoryMock.Setup(rep => rep.GetByIdAsync(id)).Returns(Task.FromResult(category));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.GetCategory(id);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(category);
        }

        [Fact]
        async public Task GetCategory_Returns_NotFound()
        {
            var id = 4;
            _categoryRepositoryMock.Setup(rep => rep.GetByIdAsync(id)).Returns(Task.FromResult(null as Category));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.GetCategory(id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        async public Task CreateCategory_Returns_BadRequest_RepeatedDescription()
        {
            var categoryDTO = new CategoryCreationDTO() { Description="Herramientas" };
            _categoryRepositoryMock.Setup(rep => rep.CategoryExist(categoryDTO.Description)).Returns(Task.FromResult(true));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.CreateCategory(categoryDTO);

            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Category already exists");
        }

        [Fact]
        async public Task CreateCategory_Returns_Ok()
        {
            var categoryDTO = new CategoryCreationDTO() { Description = "Higiene" };
            var category = new Category() { Description = "Higiene" };
            var returnedCategory = new Category() { ID = 4, Description = "Higiene" };
            _categoryRepositoryMock.Setup(rep => rep.CategoryExist(categoryDTO.Description)).Returns(Task.FromResult(false));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);
            _mapperMock.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
            _categoryRepositoryMock.Setup(rep => rep.AddAsync(category)).Returns(Task.FromResult(returnedCategory));

            var result = await _controller.CreateCategory(categoryDTO);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        async public Task UpdateCategory_Returns_BadRequest()
        {
            var id = 0;
            var categoryDTO = new CategoryCreationDTO() { Description = "Higiene" };
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.UpdateCategory(id, categoryDTO);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        async public Task UpdateCategory_Returns_NotFound()
        {
            var id = 4;
            var categoryDTO = new CategoryCreationDTO() { Description = "Higiene" };
            _categoryRepositoryMock.Setup(rep => rep.GetByIdAsync(id)).Returns(Task.FromResult(null as Category));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.UpdateCategory(id, categoryDTO);

            result.Should().BeOfType<NotFoundObjectResult>();
            result.As<NotFoundObjectResult>().Value.Should().Be("Category not found");
        }

        [Fact]
        async public Task UpdateCategory_Returns_Ok()
        {
            var id = 1;
            var categoryDTO = new CategoryCreationDTO() { Description = "Higiene" };
            var categoryToUpdate = new Category() { ID = 1, Description = "Herramientas" };
            var categoryUpdated = new Category() { ID = 1, Description = "Higiene" };
            _categoryRepositoryMock.Setup(rep => rep.GetByIdAsync(id)).Returns(Task.FromResult(categoryToUpdate));
            _categoryRepositoryMock.Setup(rep => rep.Update(categoryUpdated)).Returns(categoryUpdated);
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _mapperMock.Setup(m => m.Map<Category>(categoryDTO)).Returns(categoryUpdated);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);     

            var result = await _controller.UpdateCategory(id, categoryDTO);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        async public Task DeleteCategory_Returns_BadRequest()
        {
            var id = 0;
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.DeleteCategory(id);

            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("ID must be greater than 1");
        }

        [Fact]
        async public Task DeleteCategory_Returns_NotFound()
        {
            var id = 5;
            _categoryRepositoryMock.Setup(rep => rep.DeleteAsync(id)).Returns(Task.FromResult(false));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.DeleteCategory(id);

            result.Should().BeOfType<NotFoundObjectResult>();
            result.As<NotFoundObjectResult>().Value.Should().Be("Category not found");
        }

        [Fact]
        async public Task DeleteCategory_Returns_Ok()
        {
            var id = 1;
            _categoryRepositoryMock.Setup(rep => rep.DeleteAsync(id)).Returns(Task.FromResult(true));
            _uowMock.Setup(uow => uow.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _controller = new CategoryController(_uowMock.Object, _mapperMock.Object);

            var result = await _controller.DeleteCategory(id);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be("Category removed successfully");
        }
    }
}
