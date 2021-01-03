using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterClassService : ServiceBase
    {
        public CharacterClassService(ICharacterRepository characterRepository, IRaceRepository raceRepository, ICharacterClassRepository characterClassRepository) :
            base(characterRepository, raceRepository, characterClassRepository)
        { }

        public async Task<List<CharacterClass>> ListCharacterClasses() => await CharacterClassRepository.ListCharacterClasses();


        public async Task<CharacterClass> LookupCharacterClass(string name)
        {
            return await CharacterClassRepository.LookupCharacterClass(name);
        }

        public async Task<bool> CreateCharacterClass()
        {

        }
    }
}
