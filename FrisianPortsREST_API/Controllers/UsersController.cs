using FrisianPortsREST_API.Error_Logger;
using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ILoggerService _logger;

        public UsersController(ILoggerService logger)
        {
            _logger = logger;
        }

        UserRepository userRepo = new UserRepository();
       
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>Http Statuscode along with users</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await userRepo.Get();
                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Gets user based on received userId
        /// </summary>
        /// <param name="Id">Id of requested User</param>
        /// <returns>Http Statuscode along with requested object</returns>
        [HttpGet("{Id}")]   
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var user = await userRepo.GetById(Id); 
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Adds an item to the database
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>Http Statuscode along with created user</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                
                int newUserId = await userRepo.Add(user);

                if (newUserId > 0)
                {
                    user.User_Id = newUserId;
                    return StatusCode(StatusCodes.Status201Created, user);
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
        /// Deletes a user from the database
        /// </summary>
        /// <param name="Id">Id of user</param>
        /// <returns>Http Statuscode based on success of request</returns>
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                int success = userRepo.Delete(Id);
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
        /// Updates an existing user in the database
        /// </summary>
        /// <param name="user">Updated user in the database</param>
        /// <returns>Http Statuscode based on the response</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                int success = await userRepo.Update(user);
                
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
