using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class WeaponService : ServiceBase
    {
        public WeaponService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, ITalentRepository talentRepository, IWeaponRepository weaponRepository, IGearRepository gearRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository, talentRepository, weaponRepository, gearRepository) { }

        public async Task<List<Weapon>> ListWeapons() => await WeaponRepository.GetWeapons();

        public async Task AddWeapon(Character character, Weapon weapon)
        {
            await WeaponRepository.AddWeapon(character, weapon);
        }

        public async Task<Character> RemoveWeaponFromCharacter(Character character, Weapon weapon)
        {
            character = await WeaponRepository.RemoveWeaponFromCharacter(character, weapon);
            return character;
        }

        public async Task UpdateWeapons(List<Weapon> weapons)
        {
            await WeaponRepository.UpdateWeapons(weapons);
        }

        public async Task AddExistingWeaponToCharacter(Character character, Weapon weapon)
        {
            await WeaponRepository.AddExistingWeaponToCharacter(character, weapon);
        }
    }
}
