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
            try
            {
                var routes = await routeRepo.Get();
                return Ok(routes);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpGet("{routeId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int routeId)
        {
            try
            {
                var route = await routeRepo.GetById(routeId);
                return Ok(route);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Route route)
        {
            try
            {
                if (route == null)
                {
                    return BadRequest();
                }

                int idOfNewRoute = await routeRepo.Add(route);
                route.Route_Id = idOfNewRoute;
                
                if (idOfNewRoute > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, route);
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
        public IActionResult Delete(int routeId)
        {
            try
            {
                int success = routeRepo.Delete(routeId);
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
        public async Task<IActionResult> Update([FromBody]Route routeUpdate)
        {
            try
            {
                int success = await routeRepo.Update(routeUpdate);
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
