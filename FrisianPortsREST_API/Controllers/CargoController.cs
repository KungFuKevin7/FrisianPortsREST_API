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
            try
            {
                var cargoItems = await cargoRepo.Get();
            
                if (cargoItems == null)
                {
                    return NotFound();
                }
            
                return Ok(cargoItems);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{cargoId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int cargoId)
        {
            try
            {
                var cargoItem = await cargoRepo.GetById(cargoId);

                if (cargoItem == null)
                {
                    return NotFound();
                }

                return Ok(cargoItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Cargo cargoToAdd)
        {
            try
            {
                if (cargoToAdd == null)
                {
                    return BadRequest();
                }

                int createdId = await cargoRepo.Add(cargoToAdd);
                if (createdId > 0)
                {
                   cargoToAdd.Cargo_Id = createdId;
                   return StatusCode(StatusCodes.Status201Created,cargoToAdd);
                }
                else
                {
                    throw new Exception("Nothing was Added");
                }
     
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = cargoRepo.Delete(Id);
                if (success > 0)
                {
                    return NoContent();
                }
                else
                {
                    throw new Exception("Nothing was updated");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cargo cargo)
        {
            try
            {
                if (cargo == null) 
                {
                    return BadRequest();
                }
                
                int success = await cargoRepo.Update(cargo);
                if (success > 0)
                {
                    return NoContent();
                }
                else 
                {
                    throw new Exception("Nothing was updated");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
