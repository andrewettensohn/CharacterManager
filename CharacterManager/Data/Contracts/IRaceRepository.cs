using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IRaceRepository
    {
        public Task<List<Race>> ListRaces();

        public Task AddRace(Race characterClass);

        public Task UpdateRace(Race characterClass);

        public Task DeleteRace(Race characterClass);
    }
}
