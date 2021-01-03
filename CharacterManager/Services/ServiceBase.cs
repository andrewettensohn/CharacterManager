using CharacterManager.Data;
using CharacterManager.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class ServiceBase
    {
        protected readonly ICharacterRepository CharacterRepository;
        protected readonly IRaceRepository RaceRepository;

        protected ServiceBase(ICharacterRepository characterRepository, IRaceRepository raceRepository)
        {
            CharacterRepository = characterRepository;
            RaceRepository = raceRepository;
        }
    }
}
