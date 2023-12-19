using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo-transport")]
    public class CargoTransportDTOController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoTransportDTOController(ILoggerService logger)
        {
            _logger = logger;
        }

        CargoTransportDTORepository cargoTransportDTORepo =
            new CargoTransportDTORepository();

        /// <summary>
        /// Adds a transport and a cargo item to the database
        /// </summary>
        /// <param name="cargoTransport">Item that gets added to database</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the created resource
        /// </returns>
        [HttpPost("transport-with-cargo")]
        public async Task<IActionResult> Add([FromBody]CargoTransportDTO cargoTransport)
        {
            try
            {
                if (cargoTransport == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int createdId = await cargoTransportDTORepo.Add(cargoTransport);

                if (createdId > 0)
                {
                    cargoTransport.CargoTransportId = createdId;
                    return StatusCode(StatusCodes.Status201Created, cargoTransport);
                }
                else
                {
                    throw new Exception("Nothing was created");
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
