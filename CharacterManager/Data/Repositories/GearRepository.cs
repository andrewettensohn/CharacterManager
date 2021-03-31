using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Repositories
{
    public class GearRepository : IGearRepository
    {
        private readonly ApplicationDbContext _context;

        public GearRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> AddExistingGearToCharacter(Character character, Gear Gear)
        {
            await CreateLink(character.CharacterId, Gear.GearId);
            return character;
        }

        public async Task AddNewGear(Gear gear)
        {
            await _context.AddAsync(gear);
            await _context.SaveChangesAsync();
        }

        public async Task<Character> AddGear(Character character, Gear Gear)
        {
            await _context.AddAsync(Gear);
            await _context.SaveChangesAsync();

            await CreateLink(character.CharacterId, Gear.GearId);

            return character;
        }

        public async Task<List<Gear>> GetGearList()
        {
            return await _context.Gear.ToListAsync();
        }

        public async Task<List<Gear>> GetGearListForCharacter(int characterId)
        {
            List<Gear> Gears = new List<Gear>();
            List<GearLink> GearLinks = await _context.GearLink.Where(x => x.CharacterId == characterId).ToListAsync();

            if (!GearLinks.Any())
            {
                return Gears;
            }

            foreach (GearLink link in GearLinks)
            {
                Gear linkedGear = await _context.Gear.FirstOrDefaultAsync(x => x.GearId == link.GearId);

                if (linkedGear != null)
                {
                    Gears.Add(linkedGear);
                }
            }

            return Gears;
        }

        public async Task<Character> RemoveGearFromCharacter(Character character, Gear Gear)
        {
            character.Gear.Remove(Gear);
            GearLink link = await _context.GearLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            await RemoveExistingLink(link);

            return character;
        }

        public async Task UpdateGearList(List<Gear> Gears)
        {
            _context.UpdateRange(Gears);
            await _context.SaveChangesAsync();
        }

        private async Task<GearLink> CreateLink(int characterId, int GearId)
        {
            GearLink link = new GearLink
            {
                GearId = GearId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingLink(GearLink link)
        {
            _context.GearLink.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}
