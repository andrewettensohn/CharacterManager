using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class ArchetypeService : ServiceBase
    {
        public ArchetypeService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository) { }

        public async Task<List<Archetype>> ListArchetypes() => await ArchetypeRepository.GetArchetypes();

        public async Task<Character> SubmitArchetype(Character character)
        {
            Archetype archetype = await ArchetypeRepository.AddArchetype(character);
            character.Archetype.ArchetypeId = archetype.ArchetypeId;

            return character;
        }

        public async Task UpdateArchetype(Character character)
        {
            await ArchetypeRepository.UpdateArchetype(character);
        }
    }
}
