using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo-transport-route")]
    public class CargoTransportRouteController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoTransportRouteController(ILoggerService logger)
        {
            _logger = logger;
        }

        CargoTransportRouteRepository cargoTransportRouteRepo =
            new CargoTransportRouteRepository();

        /// <summary>
        /// Adds a CargoTransport Item as well as a Route Item to the database
        /// </summary>
        /// <param name="cargoTransportRoute">
        /// Cargotransport and route combination to add to the database
        /// </param>
        /// <returns>Corresponding HttpStatus</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoTransportRouteDTO cargoTransportRoute) 
        {
            try
            {
                if (cargoTransportRoute == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                await cargoTransportRouteRepo.Add(cargoTransportRoute);


                return StatusCode(StatusCodes.Status201Created,
                                    cargoTransportRoute);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
    }
}
