using System.ComponentModel.DataAnnotations;

namespace SystemPlusAPI.Models.Dto
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
