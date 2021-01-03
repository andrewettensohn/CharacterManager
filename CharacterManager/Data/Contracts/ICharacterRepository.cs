using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface ICharacterRepository
    {
        public Task<List<Character>> ListCharacters();

        public Task AddCharacter(Character character);

        public Task UpdateCharacter(Character character);

        public Task DeleteCharacter(Character character);

    }
}
