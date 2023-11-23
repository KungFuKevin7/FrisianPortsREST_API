using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/average")]
    public class AverageController : Controller
    {

        private readonly ILoggerService _logger;

        public AverageController(ILoggerService logger)
        {
            _logger = logger;
        }

        public AverageRepository avgRepo = new AverageRepository();

        /// <summary>
        /// Gets the average import weight of a port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <returns>Average import weight</returns>
        [HttpGet("import-tonnage")]
        public async Task<IActionResult> GetAverageImportWeightCargo(int portId)
        {
            try
            {
                var avgImport = await avgRepo.GetAverageImportWeight(portId);


                return Ok(avgImport);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the average export weight of a port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <returns>Average export weight</returns>
        [HttpGet("export-tonnage")]
        public async Task<IActionResult> GetAverageExportWeightCargo(int portId)
        {
            try
            {
                var cargo = await avgRepo.GetAverageExportWeight(portId);

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
