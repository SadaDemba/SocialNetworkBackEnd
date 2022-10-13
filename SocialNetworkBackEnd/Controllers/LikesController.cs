using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILike @ls;

        public LikesController(ILike likeService)
        {
            ls = likeService;
        }

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            return Ok(await ls.GetLikes());
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLike([FromQuery] int id)
        {
            Like? l = await ls.GetLike(id);
            if (l is null)
                return NotFound("Like not found!!!");
            return Ok(l);
        }

        // GET: api/Likes/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikesByUser([FromQuery] int id)
        {
            var l = await ls.GetLikesByUser(id);
            if (l is null)
                return NotFound("user not found!!!");
            return Ok(l);
        }


        // GET: api/Likes/5
        [HttpGet("Post/{id}")]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikesByPost([FromQuery] int id)
        {
            var l = await ls.GetLikesByPublication(id);
            if (l is null)
                return NotFound("post not found!!!");
            return Ok(l);
        }

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike([FromBody] Like like)
        {
            Like? l;
            try
            {
                l = await ls.Like(like);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(l);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{idU}/{id}")]
        public async Task<IActionResult> DeleteLike([FromQuery] int idU, [FromQuery] int id)
        {
            Like? l;
            try
            {
                l = await ls.UnLike(idU, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (l is null)
                return NotFound("Like not found!!!");
            return Ok(l);
        }
    }
}
