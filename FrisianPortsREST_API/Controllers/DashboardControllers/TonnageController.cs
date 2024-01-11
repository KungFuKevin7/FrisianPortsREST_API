using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using FrisianPortsREST_API.Repositories.Dashboard_Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/tonnage")]
    public class TonnageController : Controller
    {

        private readonly ILoggerService _logger;

        public TonnageController(ILoggerService logger)
        {
            _logger = logger;
        }

        public TonnageRepository tonnageRepo =
            new TonnageRepository();

        /// <summary>
        /// Gets amount of tonnes contibuting to the import of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to import of port</returns>
        [HttpGet("import-of-port")]
        public async Task<IActionResult> GetPortImportTonnage(int portId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetPortImportTonnage(portId, year, month);

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
        [HttpGet("export-of-port")]
        public async Task<IActionResult> GetPortExportTonnage(int portId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetPortExportTonnage(portId, year, month);

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
        /// <param name="provinceId">Id of requested province</param>
        /// <param name="period">Period to filter results (Year)</param>
        /// <returns>Number of tonnes contributing to export of province</returns>
        [HttpGet("import-of-province")]
        public async Task<IActionResult> GetImportWeightCargo(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetProvinceImportTonnage(provinceId, year, month);

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
        [HttpGet("export-of-province")]
        public async Task<IActionResult> GetExportWeightCargo(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetProvinceExportTonnage(provinceId, year, month);

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
        public async Task<IActionResult> GetTonnageInProvince(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetTonnageInsideProvince(provinceId, year, month);

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
        public async Task<IActionResult> GetImportFromProvince(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetTonnageFromOutsideProvince(provinceId, year, month);

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
        public async Task<IActionResult> GetExportToOutsideProvince(int provinceId, int year, int month)
        {
            try
            {
                var cargo = await tonnageRepo.GetTonnageToOutsideProvince(provinceId, year, month);

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
