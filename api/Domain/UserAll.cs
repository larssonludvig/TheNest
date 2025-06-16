namespace TheNestAPI.Domain
{
    public class UserAll
    {
        public string Name { get; set; } = "";
        public string? XboxName { get; set; }
        public string? PsnName { get; set; }
        public string? SteamName { get; set; }
        public string? ClubTag { get; set; }
        public List<LeaderboardEntry> Entries { get; set; }
    }
}
