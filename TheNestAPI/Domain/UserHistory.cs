namespace TheNestAPI.Domain
{
    public class UserHistory
    {
        public string Name { get; set; } = "";
        public List<int>? Ranks { get; set; } 
        public List<DateTime>? Timestamps { get; set; }
    }
}
