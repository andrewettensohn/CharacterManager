using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Repositories
{
    public class ArmorRepository : IArmorRepository
    {
        private readonly ApplicationDbContext _context;

        public ArmorRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task AddNewArmor(Armor armor)
        {
            await _context.AddAsync(armor);
            await _context.SaveChangesAsync();
        }

        public async Task<Armor> AddArmor(Character character)
        {
            await _context.AddAsync(character.Armor);
            await _context.SaveChangesAsync();

            ArmorLink link = await _context.ArmorLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            if (link != null)
            {
                await RemoveExistingLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);

            return character.Armor;
        }

        public async Task<Armor> GetArmorForCharacter(Guid characterId)
        {
            ArmorLink link = await _context.ArmorLink.FirstOrDefaultAsync(x => x.CharacterId == characterId);

            if (link is null)
            {
                return null;
            }

            return await _context.Armor.FirstOrDefaultAsync(x => x.ArmorId == link.ArmorId);
        }

        public async Task<List<Armor>> GetArmorList()
        {
            return await _context.Armor.ToListAsync();
        }

        public async Task UpdateArmor(Character character)
        {

            ArmorLink link = _context.ArmorLink.FirstOrDefault(x => x.CharacterId == character.CharacterId);

            if (link != null)
            {
                await RemoveExistingLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Armor.ArmorId);

            _context.Entry(character.Armor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<ArmorLink> CreateLink(Guid characterId, Guid armorId)
        {
            ArmorLink link = new ArmorLink
            {
                ArmorId = armorId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingLink(Guid characterId)
        {

            ArmorLink link = _context.ArmorLink.FirstOrDefault(x => x.CharacterId == characterId);

            _context.ArmorLink.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}
