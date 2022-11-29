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
            var LoginResponse = await _userRepo.Login(loginRequestDTO);
            if (LoginResponse.Token == null || string.IsNullOrEmpty(LoginResponse.Token))
            {
                return BadRequest(new { message = "Username or password is incorrect"});
            }
            return Ok("Logged in succesfully");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
           var user = _userRepo.Register(registrationRequestDTO);
            return Ok("Sucessfully registered");
        }

    }
}
