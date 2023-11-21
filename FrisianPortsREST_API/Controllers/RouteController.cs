using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
                if (routes == null)
                {
                    return NotFound();
                }

                return Ok(routes);
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
                var route = await routeRepo.GetById(Id);
                if (route == null)
                {
                    return NotFound();
                }

                return Ok(route);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);   
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
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Route existingRoute = routeRepo.CheckCombinationExists
                    (route.Departure_Port_Id, route.Arrival_Port_Id).Result;

            
                if (existingRoute == null) //If Route does not exist yet, add
                {
                    int idOfNewRoute = await routeRepo.Add(route);
                    route.Route_Id = idOfNewRoute;

                    if (idOfNewRoute > 0)
                    {
                        return StatusCode(StatusCodes.Status201Created, route);
                    }
                }
                else            //return the same Route that already existed
                {
                    return StatusCode(StatusCodes.Status303SeeOther, existingRoute);
                }

                throw new Exception("Nothing was added");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = routeRepo.Delete(Id);
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
                if (routeUpdate == null) 
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

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
