using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo")]
    public class CargoController : Controller
    {
        CargoRepository cargoRepo = new CargoRepository();


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargoItems = await cargoRepo.Get();
            return Ok(cargoItems);
        }

        [HttpGet("{cargoId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int cargoId)
        {
            var cargoItem = await cargoRepo.GetById(cargoId);
            return Ok(cargoItem);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Cargo cargoToAdd)
        {
            int success = await cargoRepo.Add(cargoToAdd);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int cargoId)
        {
            int success = cargoRepo.Delete(cargoId);
            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cargo cargo)
        {
            int success = await cargoRepo.Update(cargo);
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
