using Microsoft.AspNetCore.Mvc;
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
    }
}


