using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Contracts
{
    public interface ICharacterRepository
    {
        public Task<List<Character>> ListCharacters();

        public Task<Character> NewCharacter(Character character);

        public Task<Character> GetCharacter(Guid id);

        public Task UpdateCharacter(Character character);
    }
}
