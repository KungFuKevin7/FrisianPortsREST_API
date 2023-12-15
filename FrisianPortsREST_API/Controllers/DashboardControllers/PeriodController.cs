using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/period")]
    public class PeriodController : Controller
    {

        private readonly ILoggerService _logger;

        public PeriodController(ILoggerService logger)
        {
            _logger = logger;
        }

        public PeriodRepository periodRepo = new PeriodRepository();

        /// <summary>
        /// Gets import and export per year for requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <returns>import and export per year for requested port</returns>
        [HttpGet("yearly-report-of-port")]
        public async Task<IActionResult> GetYearlyReportPort(int portId)
        {
            try
            {
                var import = await periodRepo.getYearlyReportPort(portId);

                return Ok(import);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets import and export per year for requested port
        /// </summary>
        /// <param name="provinceId">Id of requested port</param>
        /// <returns>import and export per year for requested port</returns>
        [HttpGet("yearly-report-of-province")]
        public async Task<IActionResult> GetYearlyReportProvince(int provinceId)
        {
            try
            {
                var import = await periodRepo.getYearlyReportProvince(provinceId);

                return Ok(import);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets all the years, where data is available based on requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <returns>List of years where data is available</returns>
        [HttpGet("available-years-of-port")]
        public async Task<IActionResult> GetAvailibleYearsOfPort(int portId) 
        {
            try
            {
                var years = await periodRepo.GetAllYearsOfPort(portId);

                return Ok(years);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets all the years, where data is available based on requested province
        /// </summary>
        /// <param name="provinceId">Id of requested province</param>
        /// <returns>List of years where data is available</returns>
        [HttpGet("available-years-of-province")]
        public async Task<IActionResult> GetAvailibleYearsOfProvince(int provinceId)
        {
            try
            {
                var years = await periodRepo.GetAllYearsOfProvince(provinceId);

                return Ok(years);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
