using System.ComponentModel.DataAnnotations.Schema;

namespace SystemPlusAPI.Models.Dto
{
    public class PassengerVehicleDTO : Vehicle
    {
       public int Kw { get; set; }
    }
}
