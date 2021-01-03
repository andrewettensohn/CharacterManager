using CharacterManager.Data;
using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterService : ServiceBase
    {
        public CharacterService(ICharacterRepository characterRepository, IRaceRepository raceRepository) : base(characterRepository, raceRepository)
        { }

        public async Task<List<Character>> ListCharacters() => await CharacterRepository.ListCharacters();

    }
}
