using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/transport")]
    public class TransportController : Controller
    {
        TransportRepository TransportRepo = new TransportRepository();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transports = await TransportRepo.Get();
                if (transports == null)
                {
                    return NotFound();
                }

                return Ok(transports);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{transportId}")]
        public async Task<IActionResult> Get(int transportId)
        {
            try
            {
                var transports = await TransportRepo.GetById(transportId);
                
                if (transports == null) 
                {
                    return NotFound();
                }
                
                return Ok(transports);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Transport transport) 
        {
            try
            {
                if (transport == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int transportId = await TransportRepo.Add(transport);
                transport.Transport_Id = transportId;
                if (transportId > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, transport);
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
        public IActionResult Delete(int id)
        {
            try
            {
                int removeSuccess = TransportRepo.Delete(id);
                if (removeSuccess > 0)
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
        public async Task<IActionResult> Update([FromBody]Transport transport) 
        {
            try
            {
                if (transport == null) 
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var updateSuccess = await TransportRepo.Update(transport);
                
                if (updateSuccess > 0)
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
