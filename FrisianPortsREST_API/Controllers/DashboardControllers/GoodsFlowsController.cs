﻿using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace FrisianPortsREST_API.Controllers.DashboardControllers
{
    [Route("api/flow-of-goods")]
    public class GoodsFlowsController : Controller
    {

        private readonly ILoggerService _logger;

        public GoodsFlowsController(ILoggerService logger)
        {
            _logger = logger;
        }

        public GoodsFlowRepository goodsFlowRepo =
            new GoodsFlowRepository();

        /// <summary>
        /// Get all flow-of-goods
        /// </summary>
        /// <returns>Flow Of goods available</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCargoTransports()
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetAllGoodsFlows();

                return Ok(cargoTransports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all flow-of-goods of requested port
        /// </summary>
        /// <param name="portId">Id of requested port</param>
        /// <returns>Cargotransport connected to requested port</returns>
        [HttpGet("by-port")]
        public async Task<IActionResult> GetCargoTransports(int portId)
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetGoodsFlows(portId);

                return Ok(cargoTransports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all flow-of-goods based on search query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Flow-of-goods matching parts of the query</returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetWithSearch(string query)
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetGoodsFlows(query);

                return Ok(cargoTransports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all flow-of-goods based on search query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Flow-of-goods matching parts of the query</returns>
        [HttpGet("search/filtered")]
        public async Task<IActionResult> GetWithSearchAndFilter(string query, string[] provinces)
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetGoodsFlows(query, provinces);
                if (cargoTransports == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(cargoTransports);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets information about requested cargoTransport
        /// </summary>
        /// <param name="cargoTransportId">Id of requested Cargotransport</param>
        /// <returns>
        /// Flow-of-goods information matching the requested cargoTransport
        /// </returns>
        [HttpGet("by-id")]
        public async Task<IActionResult> GetById(int cargoTransportId)
        {
            try
            {
                var cargoTransports = await goodsFlowRepo.
                    GetGoodsFlowsById(cargoTransportId);

                return Ok(cargoTransports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
