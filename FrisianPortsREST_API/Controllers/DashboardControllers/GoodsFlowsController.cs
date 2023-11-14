using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/flow-of-goods")]
    public class GoodsFlowsController : Controller
    {
        public GoodsFlowRepository goodsFlowRepo =
            new GoodsFlowRepository();

        [HttpGet]
        public async Task<IActionResult> GetCargoTransports(int portId)
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetGoodsFlows(portId);

                return Ok(cargoTransports);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
