using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly IPublication @ps;

        public PublicationsController(IPublication publicationService)
        {
            @ps = publicationService;
        }

        // GET: api/Publications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
        {
            return Ok(await ps.GetPublications());
        }

        // GET: api/Publications
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublicationsByUser([FromQuery] int id)
        {
          var pubs=  await ps.GetPublicationsByUser(id);
            if (pubs is null)
                return NotFound("User not found!!!");
            return Ok(pubs);
        }

        // GET: api/Publications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication([FromQuery]  int id)
        {
            Publication? usr = await ps.GetPublication(id);
            if (usr is null)
                return NotFound("User not found!!!");
            return Ok(usr);
        }

        // PUT: api/Publications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idU}/{id}")]
        public async Task<IActionResult> PutPublication([FromQuery] int idU, [FromQuery] int id, [FromBody] Publication publication)
        {
            Publication? post;
            try
            {
                post = await ps.PutPublication(idU, id, publication);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (post is null)
                return NotFound("Publication's ids not conform!!");
            return Ok(post);
            
        }

        // POST: api/Publications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publication>> PostPublication([FromBody] Publication publication)
        {
            Publication? pub;
            try
            {
                pub = await ps.PostPublication(publication);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (pub is null)
                return NotFound("Post's Author not found!!!");    
            return CreatedAtAction("GetPublication", new { id = publication.PublicationId }, publication);
        }

        // DELETE: api/Publications/5
        [HttpDelete("{idU}/{id}")]
        public async Task<IActionResult> DeletePublication([FromQuery] int idU, [FromQuery] int id)
        {
            Publication? post;
            try
            {
                post = await ps.DeletePublication(idU, id);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (post is null)
                return NotFound("Post not found!!!");
            return NoContent();
        }

      
    }
}
