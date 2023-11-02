using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace FrisianPortsREST_API.Controllers
{
    [Route("api/port")]
    public class PortController : Controller
    {

        PortRepository portRepo = new PortRepository();

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var ports = await portRepo.Get();

                if (ports == null)
                {
                    return NotFound();
                }

                return Ok(ports);
            
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        [HttpGet("{portId}")]   //URI: api/port/{port-id}
        public async Task<IActionResult> GetById(int portId)
        {
            try
            {
                var port = await portRepo.GetById(portId);
                if (port == null)
                {
                    return NotFound();
                }

                return Ok(port);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Port port)
        {
            try
            {
                if (port == null)
                {
                    return BadRequest("port was null");
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var createdPort = await portRepo.Add(port);

                if (createdPort != 0)
                {
                    port.Port_Id = createdPort;
                    return StatusCode(StatusCodes.Status201Created, port);
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Port port)
        {
            try
            {
                if (port == null) 
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int success = await portRepo.Update(port);

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

        [HttpDelete]
        public IActionResult Delete(int portId)
        {
            try
            {
                int success =  portRepo.Delete(portId);
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
    }
}
