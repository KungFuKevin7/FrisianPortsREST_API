using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/cargo-distribution")]
    public class CargoDistributionController : Controller
    {
        CargoDistributionRepository cargoDistributionRepo =
            new CargoDistributionRepository();

        [HttpGet("import")]
        public async Task<IActionResult> GetImportDistribution(int portId)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetImport(portId);

                if (cargoDistribution == null)
                {
                    return NotFound();
                }

                return Ok(cargoDistribution);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> GetExportDistribution(int portId)
        {
            try
            {
                var cargo = await cargoDistributionRepo.GetExport(portId);

                if (cargo == null)
                {
                    return NotFound();
                }

                return Ok(cargo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
