using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface ICharacterClassRepository
    {
        public Task<List<CharacterClass>> ListCharacterClasses();

        public Task AddCharacterClass(CharacterClass characterClass);

        public Task UpdateCharacterClass(CharacterClass characterClass);

        public Task DeleteCharacterClass(CharacterClass characterClass);
    }
}
