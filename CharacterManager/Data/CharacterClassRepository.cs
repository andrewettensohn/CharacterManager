using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class CharacterClassRepository : ICharacterClassRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterClassRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }


        public Task AddCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CharacterClass>> ListCharacterClasses()
        {
            return await _context.CharacterClasses.ToListAsync();
        }

        public Task UpdateCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
