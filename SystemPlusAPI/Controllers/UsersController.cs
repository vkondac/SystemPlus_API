using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemPlusAPI.Data.UserRepository.Contract;
using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository user)
        {
            _userRepo = user;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepo.Login(loginRequestDTO);
            if (loginResponse.Token == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest(new { message = "Username or password is incorrect"});
            }
            return Ok(loginResponse.Token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
           var user = await _userRepo.Register(registrationRequestDTO);
            return Ok("Sucessfully registered");
        }

    }
}
