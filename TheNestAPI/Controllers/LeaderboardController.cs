using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;
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

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<LeaderboardEntry>> GetLeaderboardEntry(string name)
        {
            Leaderboard? entry = await _context.Leaderboard
                .Where(x => x.Name == name)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
            
            if (entry == null)
                return NotFound("Failed to get leaderboard by username.");

            return new LeaderboardEntry {
                Name = entry.Name,
                Rank = entry.RankScore,
                League = entry.League,
                Timestamp = entry.Timestamp
            };
        }

        [HttpGet]
        [Route("{name}/history")]
        public async Task<ActionResult<LeaderboardHistory>> GetLeaderboardHistoryOfUser(string name, [FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        {
            DateTime now = DateTime.Now;
            
            List<Leaderboard> res;
            
            if (from != null && to != null)
            {
                res = await _context.Leaderboard.Where(x =>
                    x.Name == name &&
                    x.Timestamp.HasValue &&
                    DateTime.Compare((DateTime)from, x.Timestamp.Value) <= 0 &&
                    DateTime.Compare((DateTime)to, x.Timestamp.Value) >= 0
                ).ToListAsync();
            }
            else
            {
                res = await _context.Leaderboard.Where(x =>
                    x.Name == name &&
                    x.Timestamp.HasValue &&
                    DateTime.Compare(now.AddDays(-7), x.Timestamp.Value) <= 0
                ).ToListAsync();
            }

            return new LeaderboardHistory {
                Name = name,
                Ranks = res.Select(x => x.RankScore).ToList(),
                Timestamps = res.Where(x => x.Timestamp.HasValue).Select(x => x.Timestamp.Value).ToList()
            };
        }

        [HttpGet]
        [Route("ruby/history")]
        public async Task<ActionResult<LeaderboardHistory>> GetRubyHistory([FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        {
            DateTime now = DateTime.Now;
            
            List<Leaderboard> res;
            
            if (from != null && to != null)
            {
                res = await _context.Leaderboard.Where(x =>
                    x.RankPosition == 500 &&
                    x.Timestamp.HasValue &&
                    DateTime.Compare((DateTime)from, x.Timestamp.Value) <= 0 &&
                    DateTime.Compare((DateTime)to, x.Timestamp.Value) >= 0
                ).ToListAsync();
            }
            else
            {
                res = await _context.Leaderboard.Where(x =>
                    x.RankPosition == 500 &&
                    x.Timestamp.HasValue &&
                    DateTime.Compare(now.AddDays(-7), x.Timestamp.Value) <= 0
                ).ToListAsync();
            }

            return new LeaderboardHistory {
                Name = "Ruby",
                Ranks = res.Select(x => x.RankScore).ToList(),
                Timestamps = res.Where(x => x.Timestamp.HasValue).Select(x => x.Timestamp.Value).ToList()
            };
        }
    }
}