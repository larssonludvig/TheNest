namespace TheNestAPI.Models
{
    public class Leaderboard
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public int rank_pos { get; set; }
        public int change_pos { get; set; }
        public string? steamName { get; set; } = null;
        public string? xboxName { get; set; } = null;
        public int leagueNumber { get; set; }
        public string league { get; set; } = "";
        public int rankScore { get; set; }
        public DateTime? timestamp { get; set; }
    }
}