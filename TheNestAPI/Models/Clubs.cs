using System.ComponentModel.DataAnnotations;

namespace TheNestAPI.Models
{
    public class Clubs
    {
        [Key]
        public string ClubTag { get; set; } = "";
        public int Users { get; set;}
        public int RankScore { get; set; }
        public int Position { get; set; }
        public Decimal AvgScore { get; set; }
    }
}
