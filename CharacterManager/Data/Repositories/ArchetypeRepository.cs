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

        public async Task<Archetype> AddArchetype(Character character)
        {
            await _context.AddAsync(character.Archetype);
            await _context.SaveChangesAsync();

            ArchetypeLink link = await _context.ArchetypeLink.FirstOrDefaultAsync(x => x.CharacterId == character.CharacterId);

            if(link != null)
            {
                await RemoveExistingLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);
           
            return character.Archetype;
        }

        public async Task<Archetype> GetArchetypeForCharacter(int characterId)
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

        public async Task UpdateArchetype(Character character)
        {

            ArchetypeLink link = _context.ArchetypeLink.FirstOrDefault(x => x.CharacterId == character.CharacterId);

            if(link != null)
            {
                await RemoveExistingLink(character.CharacterId);
            }

            await CreateLink(character.CharacterId, character.Archetype.ArchetypeId);

            _context.Entry(character.Archetype).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        private async Task<ArchetypeLink> CreateLink(int characterId, int archetypeId)
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

        private async Task RemoveExistingLink(int characterId)
        {

            ArchetypeLink link = _context.ArchetypeLink.FirstOrDefault(x => x.CharacterId == characterId);

            _context.ArchetypeLink.Remove(link);
            await _context.SaveChangesAsync();
        }

    }
}
