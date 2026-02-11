namespace TheNestAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string StreamId { get; set; } = "";
        public string Game { get; set; } = "";
        public string ElapsedTime { get; set; } = "";
        public int offset { get; set; }
        public bool Used { get; set; } = false;
        public DateTime? Created { get; set; }
        public string? Username { get; set; }
        public string? ClipURI { get; set; }
        public bool Processed { get; set; } = false;
        public bool Deleted { get; set; } = false;
    }
}
