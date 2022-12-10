using System.ComponentModel.DataAnnotations;

namespace SystemPlusAPI.Models.Dto
{
    public class CalcRequestDTO
    {
        [Required]
        public string VehicleYype { get; set; }
        public int Cm { get; set; }
        public int Kw { get; set; }
        public int PremiumNumber { get; set; }
        public int Year { get; set; }
        public int CarryWeight { get; set; }
    }
}
