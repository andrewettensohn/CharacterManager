using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public Task AddCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Character>> ListCharacters()
        {
            return await _context.Characters.ToListAsync();
        }

        public Task UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

    }
}
