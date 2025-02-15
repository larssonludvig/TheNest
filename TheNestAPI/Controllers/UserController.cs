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
            Leaderboard? res = await _context.Leaderboard.Where(x => x.Name == name).OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
            
            if (res == null)
                return NotFound($"User {name} does not exist.");
            
            return new User
            {
                Id = res.Id,
                Name = res.Name,
                ClubTag = res.ClubTag,
                Rank = res.RankPosition,
                RankScore = res.RankScore,
                Change = res.ChangeAmount,
                SteamName = res.SteamName,
                XboxName = res.XboxName,
                PsnName = res.PsnName,
                League = res.League
            };
        }

        [HttpGet]
        [Route("{name}/history")]
        public async Task<ActionResult<UserHistory>> GetUserHistory(string name)
        {
            List<Leaderboard> res = await _context.Leaderboard
                .Where(x =>
                    x.Name == name &&
                    x.Season != "s5"
                )
                .ToListAsync();

            if (res.Count == 0)
                return NotFound("There is no history of user: " + name);

            UserHistory user = new UserHistory();

            foreach (Leaderboard item in res)
            {
                switch (item.Season)
                {
                    case "cb1":
                        user.CB1 = item.League;
                        break;
                    case "cb2":
                        user.CB2 = item.League;
                        break;
                    case "ob":
                        user.OB = item.League;
                        break;
                    case "s1":
                        user.S1 = item.League;
                        break;
                    case "s2":
                        user.S2 = item.League;
                        break;
                    case "s3":
                        user.S3 = item.League;
                        break;
                    case "s4":
                        user.S4 = item.League;
                        break;
                    default:
                        break;
                }
            }

            return user;
        }
    }
}