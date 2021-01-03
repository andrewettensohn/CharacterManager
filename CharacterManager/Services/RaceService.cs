using CharacterManager.Data;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class RaceService : ServiceBase
    {
        public RaceService(IRepository repository) : base(repository)
        { }

        public async Task<List<Race>> ListRaces() => await Repository.ListRaces();
    }
}
