using CharacterManager.Data;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterService : ServiceBase
    {
        public CharacterService(IRepository repository) : base(repository)
        {}

        public async Task<List<Character>> ListCharacters() => await Repository.ListCharacters();

    }
}
