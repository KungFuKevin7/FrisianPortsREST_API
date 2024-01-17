using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/province")]
    public class ProvinceController : Controller
    {
        private readonly ILoggerService _logger;

        public ProvinceController(ILoggerService logger)
        {
            _logger = logger;
        }

        ProvinceRepository provinceRepo = new ProvinceRepository();

        /// <summary>
        /// Gets all the provinces available in the database
        /// </summary>
        /// <returns>
        /// Http statuscode along with the objects if request succeeds
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var provinces = await provinceRepo.Get();

                if (provinces == null)
                {
                    return NotFound();
                }

                return Ok(provinces);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Gets the province based on the corresponding Id
        /// </summary>
        /// <param name="Id">Id of the requested Item</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the requested resource
        /// </returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var province = await provinceRepo.GetById(Id);

                if (province == null)
                {
                    return NotFound();
                }

                return Ok(province);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Adds a province to the database
        /// </summary>
        /// <param name="provinceToAdd">Item that gets added to database</param>
        /// <returns>
        /// Corresponding Http Statuscode along with the created resource
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Province provinceToAdd)
        {
            try
            {
                if (provinceToAdd == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int createdId = await provinceRepo.Add(provinceToAdd);

                if (createdId > 0)
                {
                    provinceToAdd.ProvinceId = createdId;
                    return StatusCode(StatusCodes.Status201Created,
                                      provinceToAdd);
                }
                else
                {
                    throw new Exception("Nothing was Added");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes a province from the database based on the corresponding Id
        /// </summary>
        /// <param name="Id">Id of the item that should be deleted</param>
        /// <returns>Http Statuscode based on the status of the request</returns>
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = provinceRepo.Delete(Id);
                if (success > 0)
                {
                    return NoContent();
                }
                else
                {
                    throw new Exception("Nothing was updated");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Updates an already existing province in the database
        /// </summary>
        /// <param name="province">Updated item that replaces the old item</param>
        /// <returns>Http statuscode based on the status of request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Province province)
        {
            try
            {
                if (province == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int success = await provinceRepo.Update(province);

                if (success > 0)
                {
                    return NoContent();
                }
                else
                {
                    throw new Exception("Nothing was updated");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        
        /// <summary>
        /// Get all Ports within a province
        /// </summary>
        /// <param name="provinceId">Id of province to filter ports by</param>
        /// <returns>Corresponding Http StatusCode along with items found</returns>

        [HttpGet("ports")]
        public async Task<IActionResult> GetPortsInProvince(int provinceId)
        {
            try
            {
                var ports = await provinceRepo.GetPortsInProvince(provinceId);

                if (ports == null)
                {
                    return NotFound();
                }
                return Ok(ports);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
