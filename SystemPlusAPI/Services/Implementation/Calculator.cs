using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using SystemPlusAPI.Data;
using SystemPlusAPI.Models.Dto;
using SystemPlusAPI.Services.Contract;
using static SystemPlusAPI.Data.Constants;

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
            if (calcRequestDTO.VehicleYype == PassengerVehicle || calcRequestDTO.VehicleYype == MotoCycle)
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

                var commune = communes.Where(x => calcRequestDTO.Cm >= x.Min && calcRequestDTO.Cm <= x.Max)
                    .Select(x => x.Price)
                    .FirstOrDefault();

                var taxes = _config.GetSection(calcRequestDTO.VehicleYype + "Tax")
                    .GetChildren().ToList().Select(x => new
                    {
                        Min = x.GetValue<int>("minCm"),
                        Max = x.GetValue<int>("maxCm"),
                        MinYear = x.GetValue<int>("minGod"),
                        MaxYear = x.GetValue<int>("maxGod"),
                        Price = x.GetValue<int>("cena")
                    });

                var tax = taxes.Where(x => calcRequestDTO.Cm >= x.Min && calcRequestDTO.Cm <= x.Max && calcRequestDTO.Year >= x.MinYear && calcRequestDTO.Year <= x.MaxYear).
                    Select(x => x.Price).FirstOrDefault();

                var premiumNumbers = _config.GetSection(calcRequestDTO.VehicleYype + "Prem").GetChildren().ToList().Select(x => new
                {
                    MinKw = x.GetValue<int>("minKw"),
                    MaxKw = x.GetValue<int>("maxKw"),
                    Grade = x.GetValue<int>("stepen"),
                    Price = x.GetValue<int>("cena")
                });

                var premiumNumber = premiumNumbers
                    .Where(x => calcRequestDTO.Kw >= x.MinKw && calcRequestDTO.Kw <= x.MaxKw && calcRequestDTO.PremiumNumber == x.Grade)
                    .Select(x => x.Price)
                    .FirstOrDefault();

                 total = commune + tax + premiumNumber;
                if (calcRequestDTO.Sticker) { total += Prices.Sticker; }
                if (calcRequestDTO.TrafficDocument) { total += Prices.TrafficDocuments; }
                if (calcRequestDTO.Plates) { total += Prices.Plates; }

            }

            if (calcRequestDTO.VehicleYype == HeavyVehicle)
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

                var commune = communes.Where(x => calcRequestDTO.CarryWeight >= x.Min && calcRequestDTO.CarryWeight <= x.Max).
                Select(x => x.Price).FirstOrDefault();

                var taxes = _config.GetSection(calcRequestDTO.VehicleYype + "Tax")
              .GetChildren().ToList().Select(x => new
              {
                  Min = x.GetValue<int>("minCm"),
                  Max = x.GetValue<int>("maxCm"),
                  MinYear = x.GetValue<int>("minGod"),
                  MaxYear = x.GetValue<int>("maxGod"),
                  Price = x.GetValue<int>("cena")
              });

                var tax = taxes.Where(x => calcRequestDTO.Cm >= x.Min && calcRequestDTO.Cm <= x.Max && calcRequestDTO.Year >= x.MinYear && calcRequestDTO.Year <= x.MaxYear).
                    Select(x => x.Price).FirstOrDefault();
            }
            return total;
        }
    }
}
