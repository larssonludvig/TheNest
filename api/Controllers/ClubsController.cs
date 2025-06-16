using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("clubs")]
    public class ClubsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clubs>>> getListOfClubs()
        {
            List<Clubs> res = await _context.Clubs
                .OrderBy(x => x.Position)
                .ToListAsync();

            if (res.Count <= 0)
                return NotFound("No clubs found.");

            return res;
        }

        [HttpGet("{clubTag}")]
        public async Task<ActionResult<Clubs>> getClub(string clubTag)
        {
            Clubs res = await _context.Clubs
                .FirstOrDefaultAsync(x => x.ClubTag.ToLower() == clubTag.ToLower());

            if (res == null)
                return NotFound("Club not found.");

            return res;
        }
    }
}