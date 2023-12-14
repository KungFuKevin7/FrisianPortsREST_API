using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/port/total")]
    public class TotalPortsController : Controller
    {

        private readonly ILoggerService _logger;

        public TotalPortsController(ILoggerService logger)
        {
            _logger = logger;
        }

        public TotalPortsRepository totalRepo =
            new TotalPortsRepository();

        /// <summary>
        /// Gets amount of ships contributing to the import of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of ships contributing to the import</returns>
        [HttpGet("import-ship-movement")]
        public async Task<IActionResult> GetImportShips(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetImportShips(portId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships contributing to the export of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of ships contributing to the export</returns>
        [HttpGet("export-ship-movement")]
        public async Task<IActionResult> GetExportShips(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetExportShips(portId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of tonnes contibuting to the import of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to import of port</returns>
        [HttpGet("import-tonnage")]
        public async Task<IActionResult> GetImportWeightCargo(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalImportWeight(portId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of tonnes contibuting to the export of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to export of port</returns>
        [HttpGet("export-tonnage")]
        public async Task<IActionResult> GetExportWeightCargo(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalExportWeight(portId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
