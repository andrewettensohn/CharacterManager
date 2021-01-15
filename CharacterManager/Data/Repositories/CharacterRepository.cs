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

        public async Task<Character> GetCharacter(int id)
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
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
