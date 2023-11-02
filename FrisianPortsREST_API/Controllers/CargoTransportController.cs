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
