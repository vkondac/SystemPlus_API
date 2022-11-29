using Microsoft.AspNetCore.Mvc;
using SystemPlusAPI.Data.VehicleRepository.Contract;
using SystemPlusAPI.Models;

namespace SystemPlusAPI.Controllers
{
    [Route("api/SystemPlusAPI")]
    [ApiController]
    public class SystemPlusAPIController : ControllerBase
    {
        private IRepository<Vehicle> _repository;

        public SystemPlusAPIController(IRepository<Vehicle> repository)
        {
           _repository= repository;
        }

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


