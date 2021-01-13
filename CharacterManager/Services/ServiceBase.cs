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
        protected readonly ISkillsRepository SkillsRepository;

        protected ServiceBase(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository)
        {
            CharacterRepository = characterRepository;
            AttributesRepository = attributeRepository;
            SkillsRepository = skillsRepository;
        }
    }
}
