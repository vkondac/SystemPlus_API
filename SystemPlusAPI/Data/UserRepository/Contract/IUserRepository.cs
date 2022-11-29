using SystemPlusAPI.Models;
using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Data.UserRepository.Contract
{
    public interface IUserRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO login);
        Task<User> Register(RegistrationRequestDTO registration);
    }
}
