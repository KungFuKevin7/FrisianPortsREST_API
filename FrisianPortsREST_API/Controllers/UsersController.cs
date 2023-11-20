using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        UserRepository userRepo = new UserRepository();

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
