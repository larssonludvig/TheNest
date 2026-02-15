using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public NotesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            string authToken = Request.Headers["Authorization"];
            string storedToken = await _context.Generic
                .Where(x => x.Key == "notesAuth")
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            if (storedToken == null || authToken != storedToken)
            {
                return Unauthorized("Invalid auth token.");
            }

            return await GetNonDeletedNotes();
        }

        [HttpPut]
        public async Task<ActionResult<List<Note>>> createNote([FromBody] Note note)
        {
            string authToken = Request.Headers["Authorization"];
            string storedToken = await _context.Generic
                .Where(x => x.Key == "notesAuth")
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            if (storedToken == null || authToken != storedToken)
            {
                return Unauthorized("Invalid auth token.");
            }

            note = await addVODInfo(note);

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return await GetNonDeletedNotes();
        }

        [HttpPut("bot")]
        public async Task<ActionResult<bool>> createNoteBot([FromBody] BotNote data)
        {
            Note note = new Note
            {
                Description = data.Description,
                Username = data.Username,
                ClipURI = data.ClipURI,
                offset = 0
            };

            note = await addVODInfo(note);

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<Note> addVODInfo(Note note)
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

            DateTime start = data[0].GetProperty("started_at").GetDateTime();
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - start;
            note.ElapsedTime = $"{elapsed.Hours:D2}h{elapsed.Minutes:D2}m{elapsed.Seconds:D2}s";
            note.Game = data[0].GetProperty("game_name").GetString();
            note.Created = DateTime.Now;

            response = await client.GetAsync($"https://api.twitch.tv/helix/videos?user_id={data[0].GetProperty("user_id").GetString()}");
            content = await response.Content.ReadAsStringAsync();

            json = System.Text.Json.JsonDocument.Parse(content);
            data = json.RootElement.GetProperty("data");

            note.StreamId = data[0].GetProperty("id").ToString();

            return note;
        }

        [HttpPost("{id}/used")]
        public async Task<ActionResult<List<Note>>> ToggleUsedStatus(int id)
        {
            string authToken = Request.Headers["Authorization"];
            string storedToken = await _context.Generic
                .Where(x => x.Key == "notesAuth")
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            if (storedToken == null || authToken != storedToken)
            {
                return Unauthorized("Invalid auth token.");
            }

            var entity = await _context.Notes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Used = !entity.Used;
                await _context.SaveChangesAsync();
                return await _context.Notes
                    .Where(x => x.Id == id)
                    .ToListAsync();
            }

            return NotFound($"No note with id \"{id}\" found.");
        }

        [HttpPost("{id}/processed")]
        public async Task<ActionResult<List<Note>>> ToggleProcessedStatus(int id)
        {
            string authToken = Request.Headers["Authorization"];
            string storedToken = await _context.Generic
                .Where(x => x.Key == "notesAuth")
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            if (storedToken == null || authToken != storedToken)
            {
                return Unauthorized("Invalid auth token.");
            }

            var entity = await _context.Notes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Processed = !entity.Processed;
                await _context.SaveChangesAsync();
                return await _context.Notes
                    .Where(x => x.Id == id)
                    .ToListAsync();
            }

            return NotFound($"No note with id \"{id}\" found.");
        }

        [HttpPost("{id}/delete")]
        public async Task<ActionResult<List<Note>>> ToggleDeletedStatus(int id)
        {
            string authToken = Request.Headers["Authorization"];
            string storedToken = await _context.Generic
                .Where(x => x.Key == "notesAuth")
                .Select(x => x.Value)
                .FirstOrDefaultAsync();

            if (storedToken == null || authToken != storedToken)
            {
                return Unauthorized("Invalid auth token.");
            }

            var entity = await _context.Notes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Deleted = !entity.Deleted;
                await _context.SaveChangesAsync();
                return await GetNonDeletedNotes();
            }

            return NotFound($"No note with id \"{id}\" found.");
        }

        [HttpGet("{user}")]
        public async Task<ActionResult<int>> GetUserNoteUsedCount(string user)
        {
            return await _context.Notes
                .Where(
                    x => x.Username.ToLower() == user.ToLower() &&
                    x.Used == true &&
                    x.Deleted == false
                ).CountAsync();
        }

        private async Task<List<Note>> GetNonDeletedNotes()
        {
            return await _context.Notes
                .Where(x =>
                    x.Deleted == false &&
                    (DateTime.Compare(DateTime.Now.AddDays(-14), x.Created ?? DateTime.Now.AddDays(-15)) <= 0 || x.ClipURI != null)
                )
                .ToListAsync();
        }

        private bool LessThanTwoWeeks(DateTime date)
        {
            return DateTime.Compare(DateTime.Now.AddDays(-14), date) <= 0;
        }
    }
}
