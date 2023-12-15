using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("cargo-transport-route")]
    public class CargoTransportRouteController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoTransportRouteController(ILoggerService logger)
        {
            _logger = logger;
        }

        CargoTransportRouteRepository cargoTransportRouteRepo =
            new CargoTransportRouteRepository();

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

                int createdId = await cargoTransportRouteRepo.Add(cargoTransportRoute);

                if (createdId > 0)
                {
                    //cargoTransportRoute.ProvinceId = createdId;
                    return StatusCode(StatusCodes.Status201Created,
                                      cargoTransportRoute);
                }
                else
                {
                    throw new Exception("Nothing was Added");
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
