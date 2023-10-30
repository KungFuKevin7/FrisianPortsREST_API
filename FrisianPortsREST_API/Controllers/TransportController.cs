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
            var transports = await TransportRepo.Get();
            return Ok(transports);
        }

        [HttpGet("{transport-id}")]
        public async Task<IActionResult> Get(int portId)
        {
            var transports = await TransportRepo.GetById(portId);
            return Ok(transports);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Transport transport) 
        {
            var addSuccess = await TransportRepo.Add(transport);
            if (addSuccess > 0)
            {
                return Ok(addSuccess);
            }
            else {
                return BadRequest(); 
            }
        }

        [HttpDelete]
        public IActionResult Delete(int transportId)
        {
            var removeSuccess = TransportRepo.Delete(transportId);
            return Ok(removeSuccess);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Transport transport) 
        {
            var updateSuccess = await TransportRepo.Update(transport);
            if (updateSuccess > 0)
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
