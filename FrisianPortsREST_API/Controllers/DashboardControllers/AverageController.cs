using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/average")]
    public class AverageController : Controller
    {

        public AverageRepository avgRepo = new AverageRepository();

        [HttpGet("import-tonnage")]
        public async Task<IActionResult> GetAverageImportWeightCargo(int portId)
        {
            try
            {
                var cargo = await avgRepo.GetAverageImportWeight(portId);


                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("export-tonnage")]
        public async Task<IActionResult> GetAverageExportWeightCargo(int portId)
        {
            try
            {
                var cargo = await avgRepo.GetAverageExportWeight(portId);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
