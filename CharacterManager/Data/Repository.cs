using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public Task AddCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Task AddCharacterAction(CharacterAction characterAction)
        {
            throw new NotImplementedException();
        }

        public Task AddCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public Task AddRace(Race characterClass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacterAction(CharacterAction characterAction)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRace(Race characterClass)
        {
            throw new NotImplementedException();
        }

        public Task<List<CharacterAction>> ListCharacterActions()
        {
            throw new NotImplementedException();
        }

        public Task<List<CharacterClass>> ListCharacterClasses()
        {
            throw new NotImplementedException();
        }

        public Task<List<Character>> ListCharacters()
        {
            return _context.Characters.ToListAsync();
        }

        public Task<List<Race>> ListRaces()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterAction(CharacterAction characterAction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharacterClass(CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRace(Race characterClass)
        {
            throw new NotImplementedException();
        }
    }
}
