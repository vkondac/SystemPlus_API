using SystemPlusAPI.Models.Dto;
using SystemPlusAPI.Services.Contract;

namespace SystemPlusAPI.Services.Implementation
{
    public class Calculator : ICalculator
    {
        private readonly IConfiguration configuration;
        public Calculator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public float Calculate(CalcRequestDTO calcRequestDTO)
        {
            var text = configuration.GetValue<string>("kw:100");
            return 3;
        }
      
    }
}
