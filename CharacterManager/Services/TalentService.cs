using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class TalentService : ServiceBase
    {
        public TalentService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, ITalentRepository talentRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository, talentRepository) { }

        public async Task<List<Talent>> ListTalents() => await TalentRepository.GetTalents();

        public async Task AddTalent(Character character, Talent talent)
        {
            await TalentRepository.AddTalent(character, talent);
            await CharacterRepository.UpdateCharacter(character);
        }

        public async Task<List<Talent>> GetTalentsForCharacter(Character character)
        {
            return await TalentRepository.GetTalentsForCharacter(character.CharacterId);
        }

        public async Task UpdateTalent(Talent talent)
        {
            await TalentRepository.UpdateTalent(talent);
        }

        public async Task<Character> RemoveTalentFromCharacter(Character character, Talent talent)
        {
            character = await TalentRepository.RemoveTalentFromCharacter(character, talent);
            await CharacterRepository.UpdateCharacter(character);

            return character;
        }

        public async Task<Character> DeleteTalent(Character character, Talent talent)
        {
            character.Talents.Remove(talent);
            await TalentRepository.RemoveTalent(talent);

            return character;
        }

        public async Task UpdateTalents(List<Talent> talents)
        {
            await TalentRepository.UpdateTalents(talents);
        }

        public async Task AddExistingTalentToCharacter(Character character, Talent talent)
        {
            character = await TalentRepository.AddExistingTalentToCharacter(character, talent);
            await CharacterRepository.UpdateCharacter(character);
        }
    }
}
