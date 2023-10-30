using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Route = FrisianPortsREST_API.Models.Route;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/route")]
    public class RouteController : Controller
    {
        public RouteRepository routeRepo = new RouteRepository();


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var routes = await routeRepo.Get();
            return Ok(routes);
        }

        [HttpGet("{routeId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int routeId)
        {
            var route = await routeRepo.GetById(routeId);
            return Ok(route);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Route route)
        {
            int success = await routeRepo.Add(route);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();    //TODO: Return Proper error
            }
        }

        [HttpDelete]
        public IActionResult Delete(int routeId)
        {
            int success = routeRepo.Delete(routeId);
            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Route routeUpdate)
        {
            int success = await routeRepo.Update(routeUpdate);
            if (success > 0)
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
