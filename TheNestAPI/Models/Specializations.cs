namespace TheNestAPI.Models
{
    public class Specializations
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public bool? heavy { get; set; }
        public bool? medium { get; set; }
        public bool? light { get; set; }
    }
}