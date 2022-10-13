using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Models;
using SocialNetworkBackEnd.Interfaces;


namespace SocialNetworkBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser @us;

        public UsersController(IUser userService)
        {
            @us = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult <IEnumerable<User>>> GetUsers()
        {
            IEnumerable < User > users = await us.GetUsers();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            User? usr = await us.GetUserById(id);
            if (usr is null)
                return NotFound("User not found!!!");
            return Ok(usr);
        }

        // GET: api/Users/5
        [HttpGet("/Email")]
        public async Task<ActionResult<User>> GetUser([FromQuery] string email)
        {

            User? usr = await us.GetUserByEmail(email);
            if (usr is null)
                return NotFound("User not found!!!");
            return Ok(usr);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,[FromBody] User user)
        {
            User? usr;
            try
            {
                usr = await us.PutUser(id, user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            if (usr is null)
                return NotFound("operation denied!!!");
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                await us.PostUser(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? usr = await us.DeleteUser(id);
            if (usr is null)
                return NotFound("User not found!!!");
            return NoContent();
            
        }

    }
}
