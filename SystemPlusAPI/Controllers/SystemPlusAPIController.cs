using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SystemPlusAPI.Data;
using SystemPlusAPI.Models;
using SystemPlusAPI.Models.Dto;

namespace SystemPlusAPI.Controllers
{
    [Route("api/SystemPlusAPI")]
    [ApiController]
    public class SystemPlusAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VehicleDTO>> GetVehicles()
        {
            return Ok(VehicleStore.vehicles);
        }
        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VehicleDTO> GetSingleVehicle(int id)
        {   
            if(id== 0)
            {
                return BadRequest();
            }
            var vehicle = VehicleStore.vehicles.FirstOrDefault(x => x.Id == id);
            if(vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VehicleDTO> CreateVehicle([FromBody]VehicleDTO vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            if(vehicle.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            vehicle.Id = VehicleStore.vehicles.Max(x => x.Id)+1;
            VehicleStore.vehicles.Add(vehicle);
            return Ok(vehicle);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VehicleDTO> DeleteVehicle(int id)
        {
            var vehicle = VehicleStore.vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound("Vehicle with that id does not exist");
            }
            if(id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            VehicleStore.vehicles.Remove(vehicle);
            return Ok(vehicle);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VehicleDTO> EditVehicle(int id, [FromBody] VehicleDTO vehicleDTO)
        {
            var vehicle = VehicleStore.vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicleDTO == null)
            {
                return NotFound("Vehicle with that id does not exist");
            }
            vehicle.PremiumNumber = vehicleDTO.PremiumNumber;
            vehicle.Year= vehicleDTO.Year;
            vehicle.Cm = vehicleDTO.Cm;
            vehicle.Kw= vehicleDTO.Kw;
            return Ok(vehicle);
        }
    }
}


