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

    }
}
