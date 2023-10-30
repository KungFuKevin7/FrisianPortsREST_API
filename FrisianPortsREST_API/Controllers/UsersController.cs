using FrisianPortsREST_API.Models;
using FrisianPortsREST_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FrisianPortsREST_API.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        UserRepository userRepo = new UserRepository();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userRepo.Get();
            return Ok(users);
        }

        [HttpGet("{userId}")]   //URI: api/port/{portId}
        public async Task<IActionResult> Get(int userId)
        {
            var user = await userRepo.GetById(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Users userId)
        {
            int success = await userRepo.Add(userId);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();    //TODO: Return Proper error
            }
        }

        [HttpDelete]
        public IActionResult Delete(int userId)
        {
            int success = userRepo.Delete(userId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Users user)
        {
            int success = await userRepo.Update(user);
            if (success > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
