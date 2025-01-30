using Microsoft.EntityFrameworkCore;
using TheNestAPI.Models;

namespace TheNestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Leaderboard> Leaderboard { get; set; }
    }
}