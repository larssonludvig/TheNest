namespace TheNestAPI.Models
{
    public class Specializations
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool? Heavy { get; set; }
        public bool? Medium { get; set; }
        public bool? Light { get; set; }
    }
}