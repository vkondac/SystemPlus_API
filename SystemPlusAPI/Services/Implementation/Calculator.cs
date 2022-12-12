using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using SystemPlusAPI.Data;
using SystemPlusAPI.Models.Dto;
using SystemPlusAPI.Services.Contract;

namespace SystemPlusAPI.Services.Implementation
{
    public class Calculator : ICalculator
    {
        private readonly IConfiguration _config;
        public Calculator(IConfiguration configuration)
        {
            _config = configuration;
        }
        public dynamic Calculate(CalcRequestDTO calcRequestDTO)
        {
            int total = 0;
            int commune = 0;
            int tax = 0;
           if(calcRequestDTO.VehicleYype == Constants.PassengerVehicle || calcRequestDTO.VehicleYype == Constants.MotoCycle)
            {
                var communes = _config.GetSection(calcRequestDTO.VehicleYype)
                  .GetChildren()
                  .ToList()
                  .Select(x => new
                  {
                      Min = x.GetValue<int>("minCm"),
                      Max = x.GetValue<int>("maxCm"),
                      Price = x.GetValue<int>("cena")
                  });
                foreach (var item in communes)
                {
                    if (calcRequestDTO.Cm >= item.Min && calcRequestDTO.Cm <= item.Max)
                    {
                        commune = item.Price;
                    }
                }
                var taxes = _config.GetSection(calcRequestDTO.VehicleYype + "Tax")
                    .GetChildren().ToList().Select(x => new
                    {
                        Min = x.GetValue<int>("minCm"),
                        Max = x.GetValue<int>("maxCm"),
                        MinYear = x.GetValue<int>("minGod"),
                        MaxYear = x.GetValue<int>("maxGod"),
                        Price = x.GetValue<int>("cena")
                    });
                foreach(var item in taxes)
                {
                    if(calcRequestDTO.Cm >= item.Min && calcRequestDTO.Cm <= item.Max)
                    {
                        if(calcRequestDTO.Year >= item.MinYear && calcRequestDTO.Year <= item.MaxYear)
                        {
                            tax = item.Price;
                        }
                    }
                }

            }
            
           if(calcRequestDTO.VehicleYype == Constants.HeavyVehicle)
                {
                    var communes = _config.GetSection(calcRequestDTO.VehicleYype)
                       .GetChildren()
                       .ToList()
                       .Select(x => new
                       {
                           Min = x.GetValue<int>("minCm"),
                           Max = x.GetValue<int>("maxCm"),
                           Price = x.GetValue<int>("cena")
                       });
                    foreach (var item in communes)
                    {
                        if (calcRequestDTO.CarryWeight >= item.Min && calcRequestDTO.CarryWeight <= item.Max)
                        {
                              commune = item.Price;
                        }
                    }
                }
            return tax;
        }
    }
}
