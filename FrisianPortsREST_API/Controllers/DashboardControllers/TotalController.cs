using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/total")]
    public class TotalController : Controller
    {
        public TotalRepository totalRepo =
            new TotalRepository();

        [HttpGet("import")]
        public async Task<IActionResult> GetImportShips(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetImportShips(portId, period);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> GetExportShips(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetExportShips(portId, period);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("import-tonnage")]
        public async Task<IActionResult> GetImportWeightCargo(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalImportWeight(portId, period);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("export-tonnage")]
        public async Task<IActionResult> GetExportWeightCargo(int portId, int period)
        {
            try
            {
                var cargo = await totalRepo.GetTotalExportWeight(portId, period);

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
