using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Backend.WebAPI.Dto;
using Backend.WebAPI.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonController(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
        }

        [HttpGet]
        async public Task<IActionResult> GetAll()
        {
            var people = await _uow.PersonRepository.GetAllAsync();
            var results = _mapper.Map<IList<PersonDTO>>(people);
            return Ok(results);
        }

        [HttpGet("{id}")]
        async public Task<IActionResult> GetPerson(int id)
        {
            var person = await _uow.PersonRepository.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<PersonDTO>(person);
            return Ok(result);
        }

        [HttpPost]
        async public Task<IActionResult> CreatePerson(PersonCreationDTO createPerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exist = await _uow.PersonRepository.EmailExistAsync(createPerDTO.Email);
            if (exist)
            {
                return BadRequest("There is a person with that email already");
            }

            var person = _mapper.Map<Person>(createPerDTO);
            var result = await _uow.PersonRepository.AddAsync(person);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> UpdatePerson(int id, PersonCreationDTO editPerDTO)
        {
            if (!ModelState.IsValid || id < 1)
                return BadRequest(ModelState);

            var person = await _uow.PersonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound("Person not found");
            }
            _mapper.Map(editPerDTO, person);
            var result = _uow.PersonRepository.Update(person);
            await _uow.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        async public Task<IActionResult> DeletePerson(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be greater than 1");
            }

            var person = await _uow.PersonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound("Person not found");
            }

            var result = await _uow.PersonRepository.DeleteAsync(id);
            await _uow.SaveChangesAsync();

            return Ok(new { msg = "Person removed successfully" });
        }
    }
}
