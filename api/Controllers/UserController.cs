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
            LeaderboardLastWeek? res = await _context.LeaderboardLastWeek
                .Where(x => x.Name == name)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
            
            if (res == null)
                return NotFound($"User {name} does not exist.");

            Leagues league = await _context.Leagues
                .Where(x => x.Id == res.LeagueNumber)
                .FirstOrDefaultAsync();

            Users user = await _context.Users
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
            
            return new User
            {
                Id = res.Id,
                Name = res.Name,
                ClubTag = user.ClubTag,
                Rank = res.RankPosition,
                RankScore = res.RankScore,
                Change = res.ChangeAmount,
                SteamName = user.SteamName,
                XboxName = user.XboxName,
                PsnName = user.PsnName,
                League = league.Name
            };
        }

        [HttpGet]
        [Route("{name}/history")]
        public async Task<ActionResult<UserHistory>> GetUserHistory(string name)
        {
            return NotFound();
            // List<Leaderboard> res = await _context.Leaderboard
            //     .Where(x => x.Name == name)
            //     .ToListAsync();

            // if (res.Count == 0)
            //     return NotFound("There is no history of user: " + name);

            // UserHistory user = new UserHistory();

            // foreach (Leaderboard item in res)
            // {
            //     if (string.IsNullOrEmpty(item.Season))
            //         break;

            //     Leagues league = await _context.Leagues
            //         .Where(x => x.Id == item.LeagueNumber)
            //         .FirstOrDefaultAsync();

            //     switch (item.Season)
            //     {
            //         case "cb1":
            //             user.CB1 = league.Name;
            //             break;
            //         case "cb2":
            //             user.CB2 = league.Name;
            //             break;
            //         case "ob":
            //             user.OB = league.Name;
            //             break;
            //         case "s1":
            //             user.S1 = league.Name;
            //             break;
            //         case "s2":
            //             user.S2 = league.Name;
            //             break;
            //         case "s3":
            //             user.S3 = league.Name;
            //             break;
            //         case "s4":
            //             user.S4 = league.Name;
            //             break;
            //         case "s5":
            //             user.S5 = league.Name;
            //             break;    
            //         default:
            //             break;
            //     }
            // }

            // return user;
        }
    }
}
