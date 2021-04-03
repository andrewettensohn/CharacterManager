using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IArchetypeRepository
    {
        Task AddNewArchetype(Archetype archetype);

        Task<Character> UpdateArchetype(Character character);

        Task<Archetype> GetArchetypeForCharacter(int characterId);

        Task<List<Archetype>> GetArchetypes();

    }
}
