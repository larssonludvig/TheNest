using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheNestAPI.Data;
using TheNestAPI.Models;
using TheNestAPI.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNestAPI.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("randomizer")]
        public async Task<ActionResult<Loadout>> GetRandomLoadout(
            [FromHeader] List<string> removed_classes,
            [FromHeader] List<string> removed_specializations,
            [FromHeader] List<string> removed_weapons,
            [FromHeader] List<string> removed_gadgets
        )
        {
            // Build
            List<Builds> builds = await _context.Builds.Where(b => !removed_classes.Contains(b.Name)).ToListAsync();
            
            if (builds.Count == 0)
                return NotFound("No builds available after filtering");

            Random random = new Random();
            Builds build = builds[random.Next(builds.Count)];

            // Fetch build data
            List<Weapons> weapons;
            List<Specializations> specializations;
            List<Gadgets> gadgets;
            switch (build.Name)
            {
                case "Light":
                    specializations = await _context.Specializations.Where(s => s.Light == true && !removed_specializations.Contains(s.Name)).ToListAsync();
                    weapons = await _context.Weapons.Where(s => s.Light == true && !removed_weapons.Contains(s.Name)).ToListAsync();
                    gadgets = await _context.Gadgets.Where(s => s.Light == true && !removed_gadgets.Contains(s.Name)).ToListAsync();
                    break;
                case "Medium":
                    specializations = await _context.Specializations.Where(s => s.Medium == true && !removed_specializations.Contains(s.Name)).ToListAsync();
                    weapons = await _context.Weapons.Where(s => s.Medium == true && !removed_weapons.Contains(s.Name)).ToListAsync();
                    gadgets = await _context.Gadgets.Where(s => s.Medium == true && !removed_gadgets.Contains(s.Name)).ToListAsync();
                    break;
                case "Heavy":
                    specializations = await _context.Specializations.Where(s => s.Heavy == true && !removed_specializations.Contains(s.Name)).ToListAsync();
                    weapons = await _context.Weapons.Where(s => s.Heavy == true && !removed_weapons.Contains(s.Name)).ToListAsync();
                    gadgets = await _context.Gadgets.Where(s => s.Heavy == true && !removed_gadgets.Contains(s.Name)).ToListAsync();
                    break;
                default:
                    return NotFound("Failed to fetch build data based on generated build.");
            }

            // Error checking
            if (specializations.Count <= 0)
                return NotFound("No specialization available after filtering");
            if (weapons.Count <= 0)
                return NotFound("No weapon available after filtering");
            if (gadgets.Count < 3)
                return NotFound("No, or not enough gadgets are available after filtering");

            // Randomize Weapon and Specialization
            Specializations specialization = specializations[random.Next(specializations.Count)];
            Weapons weapon = weapons[random.Next(weapons.Count)];
            
            // Randomize three Gadgets
            List<string> selectedGadgets = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                Gadgets g = gadgets[random.Next(gadgets.Count)];
                selectedGadgets.Add(g.Name);
                gadgets.Remove(g);
            }

            return new Loadout
            {
                Class = build.Name,
                Weapon = weapon.Name,
                Specialization = specialization.Name,
                Gadgets = selectedGadgets
            };
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, Dictionary<string, List<string>>>>> GetAll()
        {
            List<string> classes = new List<string>() {"Heavy", "Medium", "Light"};

            Dictionary<string, List<string>> heavy = new Dictionary<string, List<string>>();
            heavy.Add("Specializations", (await _context.Specializations
                .Where(x => x.Heavy == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            heavy.Add("Weapons", (await _context.Weapons
                .Where(x => x.Heavy == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            heavy.Add("Gadgets", (await _context.Gadgets
                .Where(x => x.Heavy == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            
            Dictionary<string, List<string>> medium = new Dictionary<string, List<string>>();
            medium.Add("Specializations", (await _context.Specializations
                .Where(x => x.Medium == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            medium.Add("Weapons", (await _context.Weapons
                .Where(x => x.Medium == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            medium.Add("Gadgets", (await _context.Gadgets
                .Where(x => x.Medium == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );

            Dictionary<string, List<string>> light = new Dictionary<string, List<string>>();
            light.Add("Specializations", (await _context.Specializations
                .Where(x => x.Light == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            light.Add("Weapons", (await _context.Weapons
                .Where(x => x.Light == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );
            light.Add("Gadgets", (await _context.Gadgets
                .Where(x => x.Light == true)
                .ToListAsync())
                .Select(x => x.Name.ToString()).ToList()
            );

            Dictionary<string, Dictionary<string, List<string>>> res = new Dictionary<string, Dictionary<string, List<string>>>();
            res.Add("Heavy", heavy);
            res.Add("Medium", medium);
            res.Add("Light", light);

            return res;
        }

        [HttpGet]
        [Route("heavy/specializations")]
        public async Task<ActionResult<List<string>>> GetHeavySpecializations()
        {
            List<string> res = new List<string>();

            List<Specializations> query = await _context.Specializations
                .Where(x => x.Heavy == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No specializations for heavy forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("heavy/weapons")]
        public async Task<ActionResult<List<string>>> GetHeavyWeapons()
        {
            List<string> res = new List<string>();

            List<Weapons> query = await _context.Weapons
                .Where(x => x.Heavy == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No weapons for heavy forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("heavy/gadgets")]
        public async Task<ActionResult<List<string>>> GetHeavyGadgets()
        {
            List<string> res = new List<string>();

            List<Gadgets> query = await _context.Gadgets
                .Where(x => x.Heavy == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No gadgets for heavy forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("medium/specializations")]
        public async Task<ActionResult<List<string>>> GetMediumSpecializations()
        {
            List<string> res = new List<string>();

            List<Specializations> query = await _context.Specializations
                .Where(x => x.Medium == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No specializations for medium forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("medium/weapons")]
        public async Task<ActionResult<List<string>>> GetMediumWeapons()
        {
            List<string> res = new List<string>();

            List<Weapons> query = await _context.Weapons
                .Where(x => x.Medium == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No weapons for medium forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("medium/gadgets")]
        public async Task<ActionResult<List<string>>> GetMediumGadgets()
        {
            List<string> res = new List<string>();

            List<Gadgets> query = await _context.Gadgets
                .Where(x => x.Medium == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No gadgets for medium forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("light/specializations")]
        public async Task<ActionResult<List<string>>> GetLightSpecializations()
        {
            List<string> res = new List<string>();

            List<Specializations> query = await _context.Specializations
                .Where(x => x.Light == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No specializations for light forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("light/weapons")]
        public async Task<ActionResult<List<string>>> GetLightWeapons()
        {
            List<string> res = new List<string>();

            List<Weapons> query = await _context.Weapons
                .Where(x => x.Light == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No weapons for light forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }

        [HttpGet]
        [Route("light/gadgets")]
        public async Task<ActionResult<List<string>>> GetLightGadgets()
        {
            List<string> res = new List<string>();

            List<Gadgets> query = await _context.Gadgets
                .Where(x => x.Light == true)
                .ToListAsync();

            if (query.Count <= 0)
                return NotFound("No gadgets for light forund.");

            foreach (var item in query)
            {
                res.Add(item.Name);
            }

            return res;
        }
    }
}