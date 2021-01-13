using CharacterManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class ServiceBase
    {
        protected readonly ICharacterRepository CharacterRepository;

        protected ServiceBase(ICharacterRepository characterRepository)
        {
            CharacterRepository = characterRepository;
        }
    }
}
