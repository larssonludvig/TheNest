namespace Domain
{
    public class Link
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Url { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}