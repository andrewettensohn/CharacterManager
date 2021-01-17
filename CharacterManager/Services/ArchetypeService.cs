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
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, ITalentRepository talentRepository, IWeaponRepository weaponRepository, IGearRepository gearRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository, talentRepository, weaponRepository, gearRepository) { }

        public async Task<List<Archetype>> ListArchetypes() => await ArchetypeRepository.GetArchetypes();

        public async Task<Character> SubmitArchetype(Character character)
        {
            character = await ArchetypeRepository.AddArchetype(character);
  
            await CharacterRepository.UpdateCharacter(character);

            return character;
        }

        public async Task<Character> UpdateArchetype(Character character)
        {
            character = await ArchetypeRepository.UpdateArchetype(character);
            await CharacterRepository.UpdateCharacter(character);

            return character;
        }
    }
}
