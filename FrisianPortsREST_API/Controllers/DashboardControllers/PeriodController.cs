using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/period")]
    public class PeriodController : Controller
    {

        public PeriodRepository periodRepo = new PeriodRepository();

        [HttpGet("yearly-import")]
        public async Task<IActionResult> GetYearlyImportWeight(int portId)
        {
            try
            {
                var import = await periodRepo.GetYearlyImport(portId);

                return Ok(import);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("yearly-export")]
        public async Task<IActionResult> GetYearlyExportWeight(int portId)
        {
            try
            {
                var cargo = await periodRepo.GetYearlyExport(portId);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("yearly-report")]
        public async Task<IActionResult> GetYearlyReport(int portId)
        {
            try
            {
                var result = await periodRepo.GetYearlyCollection(portId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
