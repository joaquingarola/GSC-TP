using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.Configuration;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Backend.WebAPI.Dto;
using Backend.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        async public Task<IActionResult> GetAll()
        {
            var categories = await _uow.CategoryRepository.GetAllAsync();
            // var results = _mapper.Map<IList<CategoryDTO>>(categories);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        async public Task<IActionResult> GetCategory(int id)
        {
            var category = await _uow.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            // var result = _mapper.Map<CategoryDTO>(category);
            return Ok(category);
        }

        [HttpPost]
        async public Task<IActionResult> CreateCategory(CategoryCreationDTO createCatDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exist = await _uow.CategoryRepository.CategoryExist(createCatDTO.Description);

            if (exist)
            {
                return BadRequest("Category already exists");
            }

            var category = _mapper.Map<Category>(createCatDTO);
            var result = await _uow.CategoryRepository.AddAsync(category);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> UpdateCategory(int id, CategoryCreationDTO editCatDTO)
        {
            if (!ModelState.IsValid || id < 1)
                return BadRequest(ModelState);

            var category = await _uow.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            _mapper.Map(editCatDTO, category);
            var result = _uow.CategoryRepository.Update(category);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        async public Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be greater than 1");
            }

            var result = await _uow.CategoryRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound("Category not found");
            }

            await _uow.SaveChangesAsync();
            return Ok(new { msg = "Person removed successfully" });
        }
    }
}
