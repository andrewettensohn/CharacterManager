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
    public class ArchetypeRepository : IArchetypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ArchetypeRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task AddNewArchetype(Archetype archetype)
        {
            await _context.AddAsync(archetype);
            await _context.SaveChangesAsync();
        }

        public async Task<Archetype> GetArchetypeForCharacter(Guid characterId)
        {
            ArchetypeLink link =  await _context.ArchetypeLink.FirstOrDefaultAsync(x => x.CharacterId == characterId);

            if(link is null)
            {
                return null;
            }

            return await _context.Archetype.FirstOrDefaultAsync(x => x.ArchetypeId == link.ArchetypeId);
        }

        public async Task<List<Archetype>> GetArchetypes()
        {
            return await _context.Archetype.ToListAsync();
        }

        public async Task<Character> UpdateArchetype(Character character)
        {

            ArchetypeLink link = await _context.ArchetypeLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            if(link != null)
            {
                await ResetCharacterXP(link, character); 
                await RemoveExistingLink(character.CharacterId);
            }

            UpdateCharacterXPForNewArchetype(character);

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);

            _context.Entry(character.Archetype).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return character;
        }

        private static Character UpdateCharacterXPForNewArchetype(Character character)
        {

            if (character.Archetype.AttributeBonus > 0)
            {
                character.XP += character.Archetype.AttributeBonus * 4;
            }

            if (character.Archetype.SkillBonus > 0)
            {
                character.XP += character.Archetype.SkillBonus * 2;
            }

            character.XP -= character.Archetype.XPCost;

            return character;
        }

        private async Task<Character> ResetCharacterXP(ArchetypeLink link, Character character)
        {
            Archetype oldArchetype = await _context.Archetype.FirstOrDefaultAsync(x => x.ArchetypeId == link.ArchetypeId);

            if (oldArchetype.AttributeBonus > 0)
            {
                character.XP -= oldArchetype.AttributeBonus * 4;
            }

            if (oldArchetype.SkillBonus > 0)
            {
                character.XP -= oldArchetype.SkillBonus * 2;
            }

            character.XP += oldArchetype.XPCost;

            return character;
        }


        private async Task<ArchetypeLink> CreateLink(Guid characterId, Guid archetypeId)
        {
            ArchetypeLink link = new ArchetypeLink
            {
                ArchetypeId = archetypeId,
                CharacterId = characterId
            };

            await _context.AddAsync(link);
            await _context.SaveChangesAsync();

            return link;
        }

        private async Task RemoveExistingLink(Guid characterId)
        {

            ArchetypeLink link = _context.ArchetypeLink.FirstOrDefault(x => x.CharacterId == characterId);

            _context.ArchetypeLink.Remove(link);
            await _context.SaveChangesAsync();
        }

    }
}
