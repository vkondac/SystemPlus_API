using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
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
            int tax = 0;
            switch (calcRequestDTO.VehicleYype)
            {
                case "putnicko" :
                    var taxes = _config.GetSection(calcRequestDTO.VehicleYype)
                      .GetChildren()
                      .ToList()
                      .Select(x => new {
                          Min = x.GetValue<int>("minCm"),
                          Max = x.GetValue<int>("maxCm"),
                          Price = x.GetValue<int>("cena")
                      });
                    foreach (var item in taxes)
                    {
                        if (calcRequestDTO.Cm >= item.Min && calcRequestDTO.Cm <= item.Max)
                        {
                            tax = item.Price;
                        }
                    }
                    break;
                case "motocikl":
                     taxes = _config.GetSection(calcRequestDTO.VehicleYype)
                      .GetChildren()
                      .ToList()
                      .Select(x => new {
                          Min = x.GetValue<int>("minCm"),
                          Max = x.GetValue<int>("maxCm"),
                          Price = x.GetValue<int>("cena")
                      });
                    foreach (var item in taxes)
                    {
                        if (calcRequestDTO.Cm >= item.Min && calcRequestDTO.Cm <= item.Max)
                        {
                            tax = item.Price;
                        }
                    }
                    break;
                case "teretno":
                    taxes = _config.GetSection(calcRequestDTO.VehicleYype)
                       .GetChildren()
                       .ToList()
                       .Select(x => new {
                           Min = x.GetValue<int>("minCm"),
                           Max = x.GetValue<int>("maxCm"),
                           Price = x.GetValue<int>("cena")
                       });
                    foreach (var item in taxes)
                    {
                        if (calcRequestDTO.CarryWeight >= item.Min && calcRequestDTO.CarryWeight <= item.Max)
                        {
                            tax = item.Price;
                        }
                    }
                    break;
            }


            return tax;
            
        }
      
    }
}
