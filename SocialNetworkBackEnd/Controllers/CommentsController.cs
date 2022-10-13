using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentaire @cs;

        public CommentsController(ICommentaire commentService )
        {
            cs = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return Ok(await cs.GetComments());
        }

        // GET: api/Comments/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult<Comment>> GetUserComments([FromQuery] int id)
        {
            var comm = await cs.GetCommentsByUser(id);
            if (comm is null)
                return NotFound("User not found!!!");
            return Ok(comm);
        }

        // GET: api/Comments/5
        [HttpGet("Post/{id}")]
        public async Task<ActionResult<Comment>> GetPostComments([FromQuery] int id)
        {
            var comm = await cs.GetCommentsByPublication(id);
            if (comm is null)
                return NotFound("Post not found!!!");
            return Ok(comm);
        }

        // GET: api/Comments/5
        [HttpGet("User/{idU}/Post/{idP}")]
        public async Task<ActionResult<Comment>> GetComments([FromQuery] int idU, [FromQuery] int idP)
        {
            var comm = await cs.GetCommentsByUserAndPublication(idU,idP);
            if (comm is null)
                return NotFound("User or Post not found!!!");
            return Ok(comm);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment([FromQuery] int id)
        {
            var comm = await cs.GetComment(id);
            if (comm is null)
                return NotFound("Comment not found!!!");
            return Ok(comm);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idU}/{id}")]
        public async Task<IActionResult> PutComment([FromQuery] int idU, [FromQuery] int id,[FromBody] Comment comment)
        {
            Comment? com;
            try
            {
                com = await cs.PutComment(idU, id, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (com is null)
                return NotFound("Comment's ids not conform!!!");
            return Ok(com);
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            Comment? c;
            try
            {
                c = await cs.PostComment(comment);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(c);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{idU}/{id}")]
        public async Task<IActionResult> DeleteComment([FromQuery] int idU,[FromQuery] int id)
        {
            Comment? c;
            try
            {
                c = await cs.DeleteComment(idU,id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (c is null)
                return NotFound("Missing comment!!!");
            return Ok(c);
        }
    }
}
