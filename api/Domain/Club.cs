namespace TheNestAPI.Domain
{
    public class Club
    {
        public string ClubTag { get; set; } = "";
        public List<User> Users { get; set;}
        public int RankScore { get; set; }
        public int Position { get; set; }
        public Decimal AvgScore { get; set; }
    }
}
