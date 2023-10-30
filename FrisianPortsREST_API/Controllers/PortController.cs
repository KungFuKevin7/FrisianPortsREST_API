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
            var ports = await portRepo.Get();
            return Ok(ports);
        }

        
        [HttpGet("{portId}")]   //URI: api/port/{port-id}
        public async Task<IActionResult> Get(int portId)
        {
            var port = await portRepo.GetById(portId);
            return Ok(port);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Port port)
        {
            int success = await portRepo.Add(port);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }   
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Port port)
        {
            int success = await portRepo.Update(port);
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
        public IActionResult Delete(int portId)
        {
            int success =  portRepo.Delete(portId);
            return Ok(success);
        }
    }
}
