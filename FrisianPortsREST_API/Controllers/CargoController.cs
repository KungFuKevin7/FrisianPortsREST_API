using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo")]
    public class CargoController : Controller
    {
        CargoRepository cargoRepo = new CargoRepository();

        /// <summary>
        /// Gets all the cargo items available in the database
        /// </summary>
        /// <returns>
        /// Http statuscode along with the objects if request succeeds
        /// </returns>
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

        /// <summary>
        /// Gets the cargo item based on the corresponding Id
        /// </summary>
        /// <param name="Id">Id of the requested Item</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the requested resource
        /// </returns>
        [HttpGet("{Id}")]  
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var cargoItem = await cargoRepo.GetById(Id);

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

        /// <summary>
        /// Adds an item to the database
        /// </summary>
        /// <param name="cargoToAdd">Item that gets added to database</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the created resource
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Cargo cargoToAdd)
        {
            try
            {
                if (cargoToAdd == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
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

        /// <summary>
        /// Deletes an Item from the database based on the corresponding Id
        /// </summary>
        /// <param name="Id">Id of the item that should be deleted</param>
        /// <returns>Http Statuscode based on the status of the request</returns>
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

        /// <summary>
        /// Updates an already existing item in the database
        /// </summary>
        /// <param name="cargo">Updated item that replaces the old item</param>
        /// <returns>Http statuscode based on the status of request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cargo cargo)
        {
            try
            {
                if (cargo == null) 
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
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
