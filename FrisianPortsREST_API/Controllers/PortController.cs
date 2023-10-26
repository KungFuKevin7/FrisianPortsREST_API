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
    }
}
