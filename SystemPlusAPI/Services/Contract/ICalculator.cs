using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Services.Contract
{
    public interface ICalculator
    {
        public float Calculate(CalcRequestDTO calcRequestDTO);
    }
}
