using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/ship-movements")]
    public class ShipMovementController : Controller
    {

        private readonly ILoggerService _logger;

        public ShipMovementController(ILoggerService logger)
        {
            _logger = logger;
        }

        private ShipMovementRepository shipMovementRepo = 
            new ShipMovementRepository();


        /// <summary>
        /// Gets amount of ships contributing to the import of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Number of ships contributing to the import</returns>
        [HttpGet("import-of-port")]
        public async Task<IActionResult> GetPortImport(int portId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetImportOfPort(portId, year, month);

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
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Number of ships contributing to the export</returns>
        [HttpGet("export-of-port")]
        public async Task<IActionResult> GetPortExport(int portId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetExportOfPort(portId, year, month);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships contributing to the import of requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Number of ships contributing to the import</returns>
        [HttpGet("import-of-province")]
        public async Task<IActionResult> GetProvinceImport(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetImportOfProvince(provinceId, year, month);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships contributing to the export of requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Number of ships contributing to the export</returns>
        [HttpGet("export-of-province")]
        public async Task<IActionResult> GetProvinceExport(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetExportOfProvince(provinceId, year, month);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships transporting cargo within province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>
        /// Number of ships contributing to transport
        /// within requested province
        /// </returns>
        [HttpGet("within-province")]
        public async Task<IActionResult> GetWithinProvince(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetTransportsInProvince(provinceId, year, month);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships transporting cargo to outside the province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>
        /// Number of ships contributing to transport
        /// to outside the province
        /// </returns>
        [HttpGet("to-outside-province")]
        public async Task<IActionResult> GetExportToOutsideProvince(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetTransportsToOutside(provinceId, year, month);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of ships transporting cargo from outside the province
        /// towards the requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>
        /// Number of ships contributing to transport
        /// from outside the province to the requested province
        /// </returns>
        [HttpGet("from-outside-province")]
        public async Task<IActionResult> GetImportFromOutside(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await shipMovementRepo.GetTransportsImportFromOutside(provinceId, year, month);

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
