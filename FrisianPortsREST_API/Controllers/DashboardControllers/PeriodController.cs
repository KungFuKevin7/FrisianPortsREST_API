using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/period")]
    public class PeriodController : Controller
    {

        public PeriodRepository periodRepo = new PeriodRepository();

        [HttpGet("yearly-report")]
        public async Task<IActionResult> GetYearlyReport(int portId)
        {
            try
            {
                var import = await periodRepo.getYearlyReport(portId);

                return Ok(import);
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("available-years")]
        public async Task<IActionResult> GetAvailibleYears(int portId) 
        {
            try
            {
                var years = await periodRepo.GetAllYears(portId);

                return Ok(years);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
