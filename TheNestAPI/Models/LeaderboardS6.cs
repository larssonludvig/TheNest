namespace TheNestAPI.Models
{
    public class LeaderboardS6
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int RankPosition { get; set; }
        public int ChangeAmount { get; set; }
        public int LeagueNumber { get; set; }
        public int RankScore { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Season { get; set; } = null;
    }
}