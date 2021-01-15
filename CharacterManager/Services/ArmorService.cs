using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class ArmorService : ServiceBase
    {
        public ArmorService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository) { }

        public async Task<List<Armor>> ListArmor() => await ArmorRepoistory.GetArmorList();

        public async Task<Character> SubmitArmor(Character character)
        {
            Armor armor = await ArmorRepoistory.AddArmor(character);
            character.Armor.ArmorId = armor.ArmorId;

            return character;
        }

        public async Task UpdateArmor(Character character)
        {
            await ArmorRepoistory.UpdateArmor(character);
        }

    }
}
