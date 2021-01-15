using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IArchetypeRepository
    {
        public Task<Character> AddArchetype(Character character);

        public Task<Character> UpdateArchetype(Character character);

        public Task<Archetype> GetArchetypeForCharacter(int characterId);

        public Task<List<Archetype>> GetArchetypes();

    }
}
