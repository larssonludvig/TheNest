using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [HttpGet]
        // [Route("{name}")]
        // public async Task<ActionResult<IEnumerable<Leaderboard>>> GetLeaderboard(string name, [FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        // {
        //     // if not given, set last week
        //     if (from == null || to == null)
        //     {
        //         from = DateTime.Now.AddDays(-7);
        //         to = DateTime.Now;
        //     }

        //     var temp = await _context.Leaderboard.Where(x => x.name == name && x.timestamp >= from && x.timestamp <= to).ToListAsync();
        //     return temp;
        // }
    }
}