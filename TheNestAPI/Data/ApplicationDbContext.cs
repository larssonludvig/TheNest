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
        public DbSet<Builds> Builds { get; set; }
        public DbSet<Specializations> Specializations { get; set; }
        public DbSet<Weapons> Weapons { get; set; }
        public DbSet<Gadgets> Gadgets { get; set; }
    }
}