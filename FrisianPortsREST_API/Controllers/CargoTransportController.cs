using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo-transport")]
    public class CargoTransportController : Controller
    {
        CargoTransportRepository cargoTransportRepo =
            new CargoTransportRepository();


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargoTransports = await cargoTransportRepo.Get();
            return Ok(cargoTransports);
        }

        [HttpGet("{cargo-transport-id}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int cargoTransportId)
        {
            var cargoTransports = await cargoTransportRepo.GetById(cargoTransportId);
            return Ok(cargoTransports);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoTransport cargoTransport)
        {
            int success = await cargoTransportRepo.Add(cargoTransport);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();    //TODO: Return Proper error
            }
        }
    }
}
