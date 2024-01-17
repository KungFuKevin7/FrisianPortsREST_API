using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/cargo-distribution/province")]
    public class CargoDistributionProvinceController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoDistributionProvinceController(ILoggerService logger)
        {
            _logger = logger;
        }

        CargoDistributionProvinceRepository cargoDistributionRepo =
            new CargoDistributionProvinceRepository();

        /// <summary>
        /// Gets the import cargo distribution of a province
        /// </summary>
        /// <param name="Id">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Various Cargotypes along with weights imported</returns>
        [HttpGet("import")]
        public async Task<IActionResult> GetImportDistribution(int Id, int year, int month)
        {
            try
            {
                var cargoDistribution = await cargoDistributionRepo.
                    GetImport(Id, year, month);

                if (cargoDistribution == null)
                {
                    return NotFound();
                }

                return Ok(cargoDistribution);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets the export cargo distribution of a province
        /// </summary>
        /// <param name="Id">Id of requested province</param>
        /// <param name="year">year to filter results by</param>
        /// <param name="month">month to filter results by</param>
        /// <returns>Various Cargotypes along with weights exported</returns>
        [HttpGet("export")]
        public async Task<IActionResult> GetExportDistribution(int Id, int year, int month)
        {
            try
            {
                var cargo = await cargoDistributionRepo.GetExport(Id, year, month);

                if (cargo == null)
                {
                    return NotFound();
                }

                return Ok(cargo);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Cargo transported on a cargotransport
        /// </summary>
        /// <param name="Id">Id of requested Cargotransport</param>
        /// <returns>
        /// Cargo transported, weight and description on requested cargotransport
        /// </returns>
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
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Gets Total transported cargo on cargoTransport
        /// </summary>
        /// <param name="cargoTransportId">Id of CargoTransport</param>
        /// <returns>
        /// Transported cargo from cargotransport (import and export)
        /// </returns>
        [HttpGet("total")]
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
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
