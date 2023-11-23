using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/cargo-type")]
    public class CargoTypeController : Controller
    {
        private readonly ILoggerService _logger;

        public CargoTypeController(ILoggerService logger)
        {
            _logger = logger;
        }

        public CargoTypeRepository cargoTypeRepo = new CargoTypeRepository();

        /// <summary>
        /// Gets all available cargotypes from database
        /// </summary>
        /// <returns>
        /// Http Statuscode along with all cargotypes if request was successful
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cargoTypes = await cargoTypeRepo.Get();
                
                if (cargoTypes == null)
                {
                    return NotFound();
                }
                
                return Ok(cargoTypes);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Gets cargotype with corresponding Id from database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Http status code along with requested cargotype</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var cargoType = await cargoTypeRepo.GetById(Id);
            
                if (cargoType == null)
                {
                    return NotFound();
                }
            
                return Ok(cargoType);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Adds a cargotype to the database
        /// </summary>
        /// <param name="cargoType">Cargotype to add to the database</param>
        /// <returns>Http statuscode along with newly created cargotype</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CargoType cargoType)
        {
            try
            {
                if (cargoType == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int createdId = await cargoTypeRepo.Add(cargoType);

                if (createdId > 0)
                {
                    cargoType.Cargo_Type_Id = createdId;
                    return StatusCode(StatusCodes.Status201Created, cargoType);  
                }
                else
                {
                    throw new Exception("Nothing was added");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Deletes an cargotype with the corresponding Id
        /// </summary>
        /// <param name="Id">Id of cargotype that should be deleted</param>
        /// <returns>Http Statuscode based on the status of request</returns>
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = cargoTypeRepo.Delete(Id);
                if (success > 0) 
                {
                    return NoContent();  
                }
                else
                {
                    throw new Exception("Nothing was deleted");
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Updates an already existing cargotype in the database
        /// </summary>
        /// <param name="cargoType">Updated cargotype to replace the old cargotype</param>
        /// <returns>Http Statuscode based on the status of request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]CargoType cargoType)
        {
            try
            {
                if (cargoType == null) 
                { 
                    return BadRequest(); 
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int success = await cargoTypeRepo.Update(cargoType);
                
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
    }
}
