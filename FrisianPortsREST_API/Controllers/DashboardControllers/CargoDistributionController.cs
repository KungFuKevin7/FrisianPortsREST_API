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
        public async Task<IActionResult> GetImportDistribution(int portId, int period)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetImport(portId, period);

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
        public async Task<IActionResult> GetExportDistribution(int portId, int period)
        {
            try
            {
                var cargo = await cargoDistributionRepo.GetExport(portId, period);

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

        [HttpGet("cargo-transport")]
        public async Task<IActionResult> GetCargoDistributionCargoTransport(int Id)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetDistributionByCargoTransport(Id);

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


        [HttpGet("by-cargo-type")]
        public async Task<IActionResult> GetCargoByType(int cargoTransportId)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetDistributionByCargoType(cargoTransportId);

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

        [HttpGet("total-cargo-distribution")]
        public async Task<IActionResult> getTotalDistribution(int cargoTransportId)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetDistributionByCargoType(cargoTransportId);

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
    }
}
