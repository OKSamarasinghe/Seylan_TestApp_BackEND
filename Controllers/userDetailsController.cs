using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seylan_App_backend_latest.Models;

namespace Seylan_App_backend_latest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userDetailsController : ControllerBase
    {
        private readonly DBContext _context;
        public userDetailsController(DBContext dBContext)
        {
            _context = dBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUsers()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUser(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUser(UserDetails user)
        {
            _context.UserDetails.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDetails user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetails>> DeleteUser(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.UserDetails.Any(e => e.Id == id);
        }
    }
}
