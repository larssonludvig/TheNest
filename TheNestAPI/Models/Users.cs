using System.ComponentModel.DataAnnotations;

namespace TheNestAPI.Models
{
    public class Users
    {
        [Key]
        public string Name { get; set; } = "";
        public string? SteamName { get; set; } = null;
        public string? XboxName { get; set; } = null;
        public string? PsnName { get; set; } = null;
        public string? ClubTag { get; set; } = null;
    }
}