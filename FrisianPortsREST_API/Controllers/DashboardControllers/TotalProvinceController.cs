using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/province/total")]
    public class TotalProvinceController : Controller
    {

        private readonly ILoggerService _logger;

        public TotalProvinceController(ILoggerService logger)
        {
            _logger = logger;
        }

        public TotalProvinceRepository totalRepo =
            new TotalProvinceRepository();

        /// <summary>
        /// Gets amount of ships contributing to the import of requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of ships contributing to the import</returns>
        [HttpGet("import-ship-movement")]
        public async Task<IActionResult> GetImportShips(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetImportShips(provinceId, period);

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
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of ships contributing to the export</returns>
        [HttpGet("export-ship-movement")]
        public async Task<IActionResult> GetExportShips(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetExportShips(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of tonnes contibuting to the import of requested province
        /// </summary>
        /// <param name="portId">Id of requested province</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to import of province</returns>
        [HttpGet("import-tonnage")]
        public async Task<IActionResult> GetImportWeightCargo(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalImportWeight(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets amount of tonnes contibuting to the export of requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to export of province</returns>
        [HttpGet("export-tonnage")]
        public async Task<IActionResult> GetExportWeightCargo(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalExportWeight(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Total Tonnage transported Within the requested province
        /// </summary>
        /// <param name="provinceId">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage transported inside the province</returns>
        [HttpGet("tonnage-within-province")]
        public async Task<IActionResult> GetTonnageInProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTonnageTransportInProvince(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Total Tonnage originating from outside province
        /// </summary>
        /// <param name="provinceId">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage originating from outside province</returns>
        [HttpGet("import-from-outside-province")]
        public async Task<IActionResult> GetImportFromProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetImportFromOutsideProvince(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Total Tonnage transported to outside of the province
        /// </summary>
        /// <param name="idOfProvince">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage transported to outside of the province</returns>
        [HttpGet("export-to-outside-province")]
        public async Task<IActionResult> GetExportToOutsideProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.ExportToOutsideProvince(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get ship movements within the province
        /// </summary>
        /// <param name="provinceId">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Ship movements within the provincee</returns>
        [HttpGet("transports-within-province")]
        public async Task<IActionResult> GetTransportsInProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTransportsInProvince(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get ship movements from outside the province
        /// </summary>
        /// <param name="provinceId">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Ship movements outside of the province</returns>
        [HttpGet("transports-from-outside-province")]
        public async Task<IActionResult> GetTransportsFromOutsideProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTransportsImportFromOutside(provinceId, period);

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get ship movements from outside the province
        /// </summary>
        /// <param name="provinceId">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Ship movements outside of the province</returns>
        [HttpGet("transports-to-outside-province")]
        public async Task<IActionResult> GetTransportsToOutsideProvince(int provinceId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTransportsToOutside(provinceId, period);

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
