using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<User>> GetUser(string name)
        {
            Leaderboard? res = await _context.Leaderboard.Where(x => x.name == name).FirstOrDefaultAsync();
            
            if (res == null)
                return NotFound($"User {name} does not exist.");
            
            return new User {
                Id = res.id,
                Name = res.name,
                Rank = res.rank_pos,
                SteamName = res.steamName,
                XboxName = res.xboxName,
                League = res.league
            };
        }

        [HttpGet]
        [Route("{name}/history")]
        public async Task<ActionResult<UserHistory>> GetUserHistory(string name, [FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        {
            DateTime now = DateTime.Now;
            
            List<Leaderboard> res;
            
            if (from != null && to != null)
            {
                res = await _context.Leaderboard.Where(x =>
                    x.name == name &&
                    x.timestamp.HasValue &&
                    DateTime.Compare((DateTime)from, x.timestamp.Value) <= 0 &&
                    DateTime.Compare((DateTime)to, x.timestamp.Value) >= 0
                ).ToListAsync();
            }
            else
            {
                res = await _context.Leaderboard.Where(x =>
                    x.name == name &&
                    x.timestamp.HasValue &&
                    DateTime.Compare(now.AddDays(-7), x.timestamp.Value) <= 0
                ).ToListAsync();
            }

            return new UserHistory {
                Name = name,
                Ranks = res.Select(x => x.rankScore).ToList(),
                Timestamps = res.Where(x => x.timestamp.HasValue).Select(x => x.timestamp.Value).ToList()
            };
        }
    }
}