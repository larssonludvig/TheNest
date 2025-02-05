namespace TheNestAPI.Domain
{
    public class LeaderboardHistory
    {
        public string Name { get; set; } = "";
        public List<int>? Ranks { get; set; } 
        public List<DateTime>? Timestamps { get; set; }
    }
}
