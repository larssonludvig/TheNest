namespace TheNestAPI.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? ClubTag { get; set; } = null;
        public int Rank { get; set; }
        public int RankScore { get; set; }
        public int Change { get; set; }
        public string? SteamName { get; set; } = null;
        public string? XboxName { get; set; } = null;
        public string? PsnName { get; set; } = null;
        public string League { get; set; } = "";
    }
}
