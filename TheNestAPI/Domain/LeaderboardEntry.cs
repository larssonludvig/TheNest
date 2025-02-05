namespace TheNestAPI.Domain
{
    public class LeaderboardEntry
    {
        public string Name { get; set; } = "";
        public int Rank { get; set; }
        public int Change { get; set; }
        public string League { get; set; } = "";
        public DateTime? Timestamp { get; set; }
    }
}
