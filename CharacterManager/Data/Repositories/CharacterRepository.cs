using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> GetCharacter(Guid id)
        {
            return await _context.Character.FirstOrDefaultAsync(x => x.CharacterId == id);
        }

        public async Task<List<Character>> ListCharacters()
        {
            return await _context.Character.ToListAsync();
        }

        public async Task<Character> NewCharacter(Character character)
        {

            await _context.AddAsync(character);
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task UpdateCharacter(Character character)
        {

            if (character.XP == 0 && character.Name == "New Character")
            {
                character = ModifyXPForTier(character);
            }

            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private static Character ModifyXPForTier(Character character)
        {
            if (character.Tier == 1)
            {
                character.XP = 100;
            }
            else if (character.Tier == 2)
            {
                character.XP = 200;
            }
            else if (character.Tier == 3)
            {
                character.XP = 300;
            }

            return character;
        }
    }
}
