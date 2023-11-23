using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/transport")]
    public class TransportController : Controller
    {

        private readonly ILoggerService _logger;

        public TransportController(ILoggerService logger)
        {
            _logger = logger;
        }

        TransportRepository TransportRepo = new TransportRepository();

        /// <summary>
        /// Gets all transport items from the database
        /// </summary>
        /// <returns>Http Statuscode along with all transport</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transports = await TransportRepo.Get();
                if (transports == null)
                {
                    return NotFound();
                }

                return Ok(transports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Gets transport with the corresponding Id
        /// </summary>
        /// <param name="Id">Id of requested item</param>
        /// <returns>Http Statuscode along with requested transport</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var transports = await TransportRepo.GetById(Id);
                
                if (transports == null) 
                {
                    return NotFound();
                }
                
                return Ok(transports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Adds an transport to the database
        /// </summary>
        /// <param name="transport">Item to be added to the database</param>
        /// <return>Http Statuscode along with created transport</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Transport transport) 
        {
            try
            {
                if (transport == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int transportId = await TransportRepo.Add(transport);
                transport.Transport_Id = transportId;
                if (transportId > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, transport);
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
        /// Deletes an transport from the database
        /// </summary>
        /// <param name="id">Id of transport to delete</param>
        /// <returns>Http Statuscode dependent of success</returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                int removeSuccess = TransportRepo.Delete(id);
                if (removeSuccess > 0)
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
        /// Updated an existing transport in the database
        /// </summary>
        /// <param name="transport">Updated object to replace old transport</param>
        /// <returns>Http Statuscode based on success</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Transport transport) 
        {
            try
            {
                if (transport == null) 
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var updateSuccess = await TransportRepo.Update(transport);
                
                if (updateSuccess > 0)
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
        /// Gets amount of transports connected to cargotransport
        /// </summary>
        /// <param name="Id">Id of Cargotransport</param>
        /// <returns>Http Statuscode along with response</returns>
        [HttpGet("count")]
        public async Task<IActionResult> GetCountInCargoTransport(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return BadRequest();
                }

                var transportCount = await TransportRepo.GetCountInCargoTransport(Id);

                return Ok(transportCount);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
