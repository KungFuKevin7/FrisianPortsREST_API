using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace FrisianPortsREST_API.Controllers
{
    [Route("api/ports")]
    public class PortController : Controller
    {
        private readonly ILoggerService _logger;
     
        public PortController(ILoggerService logger)
        {
            _logger = logger;
        }
        
        PortRepository portRepo = new PortRepository();

        /// <summary>
        /// Gets all available ports in the database
        /// </summary>
        /// <returns>
        /// Corresponding Http Statuscode along with all ports
        /// </returns>
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
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets a port with the corresponding Id
        /// URI: api/port/{port-id}
        /// </summary>
        /// <param name="Id">Id of the requested port</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the requested port
        /// </returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var port = await portRepo.GetById(Id);
                if (port == null)
                {
                    return NotFound();
                }

                return Ok(port);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Adds an Port to the database
        /// </summary>
        /// <param name="port">Port that should be added to database</param>
        /// <returns>Http Statuscode along with newly created port</returns>
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
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates an already existing port in the database
        /// </summary>
        /// <param name="port">updated port item</param>
        /// <returns></returns>
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
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes an port from the database with the corresponding Id
        /// </summary>
        /// <param name="Id">Id of port to be deleted</param>
        /// <returns>Http Statuscode based on status of request</returns>
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success =  portRepo.Delete(Id);
                if (success > 0)
                {
                    return NoContent();
                }
                else 
                {
                    throw new Exception("Nothing was deleted");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Gets all ports that contain the searchquery in their name
        /// </summary>
        /// <param name="query">search query to search for</param>
        /// <returns>
        /// Items containing the characters of query somewhere in their name
        /// </returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetPorts(string query)
        {
            try
            {
                var result = await portRepo.GetPorts(query);

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets all ports that contain the searchquery in their name
        /// </summary>
        /// <param name="query">search query to search for</param>
        /// <param name="provinces">provinces to filter the results</param>
        /// <returns>
        /// Items containing the characters of query somewhere in their name
        /// </returns>
        [HttpGet("name/filtered")]
        public async Task<IActionResult> GetPortsUsingFilters(string query, string[] provinces)
        {
            try
            {
                var result = await portRepo.GetPortsWithFilters(query, provinces);

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
