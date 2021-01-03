using CharacterManager.Data;
using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class RaceService : ServiceBase
    {
        public RaceService(ICharacterRepository characterRepository, IRaceRepository raceRepository) : base(characterRepository, raceRepository)
        { }

        public async Task<List<Race>> ListRaces() => await RaceRepository.ListRaces();
    }
}
