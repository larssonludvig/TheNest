namespace Domain
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public string StreamId { get; set; } = "";
        public string Game { get; set; } = "";
        public string ElapsedTime { get; set; } = "";
        public int Offset { get; set; }
        public bool Used { get; set; } = false;
    }
}
