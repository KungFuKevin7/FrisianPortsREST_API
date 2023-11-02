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

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var cargoType = await cargoTypeRepo.GetById(Id);
            
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
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
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
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = cargoTypeRepo.Delete(Id);
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
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
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
