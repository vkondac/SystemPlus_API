using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using SystemPlusAPI.Data.UserRepository.Contract;
using SystemPlusAPI.Models;
using SystemPlusAPI.Models.Dto;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SystemPlusAPI.Data.UserRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;
        public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = user
                };
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password);
            if (isValidPassword)
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                   new Claim(ClaimTypes.Name, user.Id.ToString()),
                   new Claim(ClaimTypes.Role, user.Role)
                   }),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseDTO login = new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = user
                };
                return login;
            }
            return new LoginResponseDTO()
            {
                Token = "",
                User = user
            };
        }

        public async Task<User> Register(RegistrationRequestDTO registration)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registration.Password);
            User user = new User
            {
                Id = Guid.NewGuid(),
                UserName = registration.UserName,
                Password = hashedPassword,
                Email = registration.Email,
                Name = registration.Name,
                Role = registration.Role 
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            user.Password = "";
            return user;

        }
    }
}
