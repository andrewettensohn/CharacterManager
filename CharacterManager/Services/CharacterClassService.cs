using CharacterManager.Data.Contracts;
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
    }
}
