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

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<>>> GetLeaderboard(string name, [FromQuery(Name = "from")] DateTime? from, [FromQuery(Name = "to")] DateTime? to)
        // {
        //     return null;
        // }

//             @RequestHeader(value = "removed_classes", required = false, defaultValue = "") String rC,
//             @RequestHeader(value = "removed_specializations", required = false, defaultValue = "") String rS,
//             @RequestHeader(value = "removed_weapons", required = false, defaultValue = "") String rW,
//             @RequestHeader(value = "removed_gadgets", required = false, defaultValue = "") String rG


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
    }
}