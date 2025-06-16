using System.ComponentModel.DataAnnotations;

namespace TheNestAPI.Models
{
    public class Generic
    {
        [Key]
        public string Key { get; set; } = "";
        public string Value { get; set;}
    }
}
