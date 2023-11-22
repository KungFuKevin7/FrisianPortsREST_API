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

        /// <summary>
        /// Gets all the items available in the database
        /// </summary>
        /// <returns>
        /// Http statuscode along with the objects if request succeeds
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cargoTransports = await cargoTransportRepo.Get();

                if (cargoTransports == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(cargoTransports);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the cargotransport item based on the corresponding Id
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
                var cargoTransport = await cargoTransportRepo.GetById(Id);
                if (cargoTransport == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(cargoTransport);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Adds a cargoTransport item to the database
        /// </summary>
        /// <param name="cargoTransport">Item that gets added to database</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the created resource
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoTransport cargoTransport)
        {
            try
            {
                if (cargoTransport == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int createdId = await cargoTransportRepo.Add(cargoTransport);
                
                if (createdId > 0)
                {
                    cargoTransport.Cargo_Transport_Id = createdId;
                    return StatusCode(StatusCodes.Status201Created, cargoTransport);
                }
                else
                {
                    throw new Exception("Nothing was created");
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
                int success = cargoTransportRepo.Delete(Id);
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

        /// <summary>
        /// Updates an already existing item in the database
        /// </summary>
        /// <param name="cargoTr">Updated item that replaces the old item</param>
        /// <returns>Http statuscode based on the status of request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CargoTransport cargoTr) 
        {
            try
            {
                if (cargoTr == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int success = await cargoTransportRepo.Update(cargoTr);
                if (success > 0)
                {
                    return NoContent();
                }

                throw new Exception("Nothing was updated");
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
