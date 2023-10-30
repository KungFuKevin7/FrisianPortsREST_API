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

        [HttpGet("{cargoTransportId}")]   //URI: api/port/{portId}
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

        [HttpDelete]
        public IActionResult Delete(int cargoTransportId) 
        {
            int success = cargoTransportRepo.Delete(cargoTransportId);
            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CargoTransport cargoTr) 
        {
            int success = await cargoTransportRepo.Update(cargoTr);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
