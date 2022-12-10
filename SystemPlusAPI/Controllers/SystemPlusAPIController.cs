using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using SystemPlusAPI.Data.VehicleRepository.Contract;
using SystemPlusAPI.Models;
using SystemPlusAPI.Models.Dto;
using SystemPlusAPI.Services.Contract;

namespace SystemPlusAPI.Controllers
{
    [Route("api/SystemPlusAPI")]
    [ApiController]
    public class SystemPlusAPIController : ControllerBase
    {
        private IRepository<Vehicle> _repository;
        private ICalculator _calc;

        public SystemPlusAPIController(IRepository<Vehicle> repository,ICalculator calc)
        {
           _repository = repository;
           _calc = calc;
        }
        [Authorize(Roles = "user,admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            var vehicles = _repository.GetAll().OrderByDescending(x => x.CreatedDate).Where(x => x.IsDeleted == false);
            if (vehicles == null)
            {
                return NotFound("No Vehicles Fund");
            }
            return Ok(vehicles);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Vehicle> CreateVehicle([FromBody] Vehicle vehicle)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            vehicle.Id = Guid.NewGuid();
            _repository.Add(vehicle);
            _repository.Save();
            return Ok(vehicle);
        }

        [HttpPost("calculator")]
        public dynamic TestCalculator([FromBody]CalcRequestDTO calcRequestDTO)
        {
            return _calc.Calculate(calcRequestDTO);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Vehicle> DeleteVehicle(Guid id)
        {
            var vehicle = _repository.GetSingleOrDefault(x => x.Id == id);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            vehicle.IsDeleted = true;
            _repository.Save();
            return Ok(vehicle);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Vehicle> EditVehicle(Guid id, [FromBody] Vehicle vehicleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vehicle = _repository.GetSingleOrDefault(x => x.Id == id);
            if (vehicleDTO == null)
            {
                return NotFound("Vehicle with that id does not exist");
            }
            vehicle.PremiumNumber = vehicleDTO.PremiumNumber;
            vehicle.Year = vehicleDTO.Year;
            vehicle.Cm = vehicleDTO.Cm;
            _repository.Save();
            return Ok(vehicle);
        }
    }
}


