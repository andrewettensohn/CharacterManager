using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class GearService : ServiceBase
    {
        public GearService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, ITalentRepository talentRepository, IWeaponRepository weaponRepository, IGearRepository gearRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository, talentRepository, weaponRepository, gearRepository) { }

        public async Task<List<Gear>> ListGear() => await GearRepository.GetGearList();

        public async Task AddGear(Character character, Gear gear)
        {
            await GearRepository.AddGear(character, gear);
        }

        public async Task<Character> RemoveGearFromCharacter(Character character, Gear gear)
        {
            character = await GearRepository.RemoveGearFromCharacter(character, gear);
            return character;
        }

        public async Task UpdateGearList(List<Gear> gearList)
        {
            await GearRepository.UpdateGearList(gearList);
        }

        public async Task AddExistingGearToCharacter(Character character, Gear gear)
        {
            await GearRepository.AddExistingGearToCharacter(character, gear);
        }

    }
}
