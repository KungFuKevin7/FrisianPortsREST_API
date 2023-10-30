using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo-type")]
    public class CargoTypeController : Controller
    {
        public CargoTypeRepository cargoTypeRepo = new CargoTypeRepository();


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cargoTypes = await cargoTypeRepo.Get();
            return Ok(cargoTypes);
        }

        [HttpGet("{cargoTypeId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int cargoTypeId)
        {
            var cargoType = await cargoTypeRepo.GetById(cargoTypeId);
            return Ok(cargoType);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoType cargoType)
        {
            int success = await cargoTypeRepo.Add(cargoType);
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
        public IActionResult Delete(int cargoTypeId)
        {
            int success = cargoTypeRepo.Delete(cargoTypeId);
            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CargoType cargoType)
        {
            int success = await cargoTypeRepo.Update(cargoType);
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
