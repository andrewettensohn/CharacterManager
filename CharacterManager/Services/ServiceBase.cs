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
        protected readonly IAttributeRepository AttributesRepository;

        protected ServiceBase(ICharacterRepository characterRepository, IAttributeRepository attributeRepository)
        {
            CharacterRepository = characterRepository;
            AttributesRepository = attributeRepository;
        }
    }
}
