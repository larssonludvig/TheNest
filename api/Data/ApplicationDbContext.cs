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
        public DbSet<LeaderboardS6> LeaderboardS6 { get; set; }
        public DbSet<LeaderboardS7> LeaderboardS7 { get; set; }
        public DbSet<LeaderboardS8> LeaderboardS8 { get; set; }
        public DbSet<LeaderboardS9> LeaderboardS9 { get; set; }
        public DbSet<LeaderboardLastWeek> LeaderboardLastWeek { get; set; }
        public DbSet<Builds> Builds { get; set; }
        public DbSet<Specializations> Specializations { get; set; }
        public DbSet<Weapons> Weapons { get; set; }
        public DbSet<Gadgets> Gadgets { get; set; }
        public DbSet<Leagues> Leagues { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Clubs> Clubs { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Generic> Generic { get; set; }
    }
}