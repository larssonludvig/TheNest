using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("twitch")]
    public class TwitchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TwitchController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("live")]
        public async Task<ActionResult<bool>> getLiveStatus()
        {
            using var client = new HttpClient();
            string clientId = _configuration["Twitch:ClientId"];
            string token = _configuration["Twitch:Token"];

            client.DefaultRequestHeaders.Add("Client-ID", clientId);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://api.twitch.tv/helix/streams?user_login=plopparntv");
            var content = await response.Content.ReadAsStringAsync();

            var json = System.Text.Json.JsonDocument.Parse(content);
            var data = json.RootElement.GetProperty("data");

            return data.GetArrayLength() > 0; // if array is not empty, streamer is live
        }
    }
}