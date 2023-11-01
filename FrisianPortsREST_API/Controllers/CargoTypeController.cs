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
            try
            {
                var cargoTypes = await cargoTypeRepo.Get();
                
                if (cargoTypes == null)
                {
                    return NotFound();
                }
                
                return Ok(cargoTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{cargoTypeId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int cargoTypeId)
        {
            try
            {
                var cargoType = await cargoTypeRepo.GetById(cargoTypeId);
            
                if (cargoType == null)
                {
                    return NotFound();
                }
            
                return Ok(cargoType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoType cargoType)
        {
            try
            {
                if (cargoType == null)
                {
                    return BadRequest();
                }

                int createdId = await cargoTypeRepo.Add(cargoType);
                if (createdId > 0)
                {
                    cargoType.Cargo_Type_Id = createdId;
                    return StatusCode(StatusCodes.Status201Created, cargoType);  
                }
                else
                {
                    throw new Exception("Nothing was added");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int cargoTypeId)
        {
            try
            {
                int success = cargoTypeRepo.Delete(cargoTypeId);
                if (success > 0) 
                {
                    return NoContent();  
                }
                else
                {
                    throw new Exception("Nothing was deleted");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CargoType cargoType)
        {
            try
            {
                if (cargoType == null) 
                { 
                    return BadRequest(); 
                }

                int success = await cargoTypeRepo.Update(cargoType);
                
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
