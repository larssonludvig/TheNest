namespace TheNestAPI.Models
{
    public class Leaderboard
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int RankPosition { get; set; }
        public int ChangeAmount { get; set; }
        public string? SteamName { get; set; } = null;
        public string? XboxName { get; set; } = null;
        public string? PsnName { get; set; } = null;
        public int LeagueNumber { get; set; }
        public string League { get; set; } = "";
        public int RankScore { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}