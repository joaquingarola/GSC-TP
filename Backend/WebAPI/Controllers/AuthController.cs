using AutoMapper;
using Backend.Entities;
using Backend.WebAPI.DataAccess.Repositories;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Backend.WebAPI.DTO;
using Backend.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IUnitOfWork _uow;
        private JwtService _jwtService;
        private IMapper _mapper;

        public AuthController(IUnitOfWork uow, JwtService jwtService, IMapper mapper)
        {
            _uow = uow;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        async public Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exist = await _uow.UserRepository.UserExist(registerDTO.UserName);
            if (exist)
            {
                return BadRequest("Username already exists");
            }

            registerDTO.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            var user = _mapper.Map<User>(registerDTO);

            var result = await _uow.UserRepository.AddAsync(user);
            await _uow.SaveChangesAsync();

            return Created("success", result);
        }

        [HttpPost("login")]
        async public Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _uow.UserRepository.GetByUser(loginDTO.UserName);
            if (user == null) 
                return BadRequest("Invalid Credentials");

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
                return BadRequest("Invalid Credentials");

            var jwt = _jwtService.Generate(user);

            return Ok(jwt);
        }

        [HttpGet("user")]
        async public Task<IActionResult> User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = await _uow.UserRepository.GetByIdAsync(userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[HttpPost("logout")]
        //public IActionResult Logout()
        //{
        //    Response.Cookies.Delete("jwt");

        //    return Ok(new
        //    {
        //        message = "success"
        //    });
        //}
    }
}
