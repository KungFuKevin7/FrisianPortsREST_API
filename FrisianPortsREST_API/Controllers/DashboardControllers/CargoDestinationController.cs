using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories.Dashboard_Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{

    [Route("api/cargo-destination")]
    public class CargoDestinationController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoDestinationController(ILoggerService logger)
        {
            _logger = logger;
        }

        CargoDestinationRepository cargoDestinationRepo =
            new CargoDestinationRepository();


        /// <summary>
        /// Gets the amount of import ships of a port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Various Cargotypes along with weights imported</returns>
        [HttpGet("import")]
        public async Task<IActionResult> ShipImport(int portId, int year, int month)
        {
            try
            {
                var cargoDistribution = await cargoDestinationRepo.
                    GetExportShips(portId, year, month);

                if (cargoDistribution == null)
                {
                    return NotFound();
                }

                return Ok(cargoDistribution);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Gets the amount of export ships of a port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Various Cargotypes along with weights imported</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ShipExport(int portId, int year, int month)
        {
            try
            {
                var cargoDistribution = await cargoDestinationRepo.
                    GetImportShips(portId, year, month);

                if (cargoDistribution == null)
                {
                    return NotFound();
                }

                return Ok(cargoDistribution);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
