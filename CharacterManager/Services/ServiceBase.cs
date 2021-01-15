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
        protected readonly IArchetypeRepository ArchetypeRepository;
        protected readonly IArmorRepository ArmorRepoistory;

        protected ServiceBase(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository, 
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository)
        {
            CharacterRepository = characterRepository;
            AttributesRepository = attributeRepository;
            SkillsRepository = skillsRepository;
            ArchetypeRepository = archetypeRepository;
            ArmorRepoistory = armorRepository;
        }
    }
}
