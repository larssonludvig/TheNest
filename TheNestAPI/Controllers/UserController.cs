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
            
            return new User {
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
    }
}