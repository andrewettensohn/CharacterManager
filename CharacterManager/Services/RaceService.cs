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
        public RaceService(ICharacterRepository characterRepository, IRaceRepository raceRepository, ICharacterClassRepository characterClassRepository) :
            base(characterRepository, raceRepository, characterClassRepository)
        { }

        public async Task<List<Race>> ListRaces() => await RaceRepository.ListRaces();

        public async Task<bool> CreateRace(Race race)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(race.Name))
                {
                    await RaceRepository.AddRace(race);
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Race> RaceLookup(string raceName)
        {
            return await RaceRepository.RaceLookup(raceName);
        }
    }
}
