using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Services.Contract
{
    public interface ICalculator
    {
        public dynamic Calculate(CalcRequestDTO calcRequestDTO);
    }
}
