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


        /// <summary>
        /// Gets all routes from the database
        /// </summary>
        /// <returns>Http Statuscodes along with all routes</returns>
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

        /// <summary>
        /// Gets the route based on the Id of item
        /// </summary>
        /// <param name="Id">Id of requested resource</param>
        /// <returns>Http Statuscode along with the requested route</returns>
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

        /// <summary>
        /// Adds an route to the database
        /// </summary>
        /// <param name="route">Item to be added to the database</param>
        /// <returns>Http Statuscode along with newly created route</returns>
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

        /// <summary>
        /// Delete an route with corresponding Id
        /// </summary>
        /// <param name="Id">Id of route to be deleted</param>
        /// <returns>Http Statuscode based on status of request</returns>
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

        /// <summary>
        /// Updates an already existing route in the database
        /// </summary>
        /// <param name="routeUpdate">Updated route</param>
        /// <returns>Http Statuscode based on status of request</returns>
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
