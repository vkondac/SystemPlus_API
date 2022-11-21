using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Data
{
    public static class VehicleStore
    {
        public static List<VehicleDTO> vehicles = new List<VehicleDTO>
        {
            new VehicleDTO{Cm=2000,Kw=100,PremiumNumber=1,Year=2002,Id=1},
            new VehicleDTO{Cm=2000,Kw=100,PremiumNumber=1,Year=200,Id=2}
        };
    }
}
