using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterService : ServiceBase
    {
        public CharacterService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository,
            IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, ITalentRepository talentRepository)
            : base(characterRepository, attributeRepository, skillsRepository, archetypeRepository, armorRepository, talentRepository) { }

        public async Task<List<Character>> ListCharacters() => await CharacterRepository.ListCharacters();

        public async Task<Character> GetCharacter(int id)
        {
            Character character = await CharacterRepository.GetCharacter(id);
            character.Attributes = await AttributesRepository.GetCharacterAttributes(id);
            character.Skills = await SkillsRepository.GetCharacterSkills(id);

            character.Archetype = await ArchetypeRepository.GetArchetypeForCharacter(id);
            character.Armor = await ArmorRepoistory.GetArmorForCharacter(id);
            character.Talents = await TalentRepository.GetTalentsForCharacter(id);

            return character;
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                Name = "New Character",
            };

            character = await CharacterRepository.NewCharacter(character);
            character.Attributes = await AttributesRepository.AddAttributes(new Attributes { CharacterId = character.CharacterId });
            character.Skills = await SkillsRepository.AddSkills(new Skills { CharacterId = character.CharacterId });
            
            return character;
        }

        public async Task UpdateCharacter(Character character)
        {
            await CharacterRepository.UpdateCharacter(character);
        }

        public async Task UpdateTier(Character character)
        {

            if(character.XP == 0)
            {
                if(character.Tier == 1)
                {
                    character.XP = 100;
                }
                else if(character.Tier == 2)
                {
                    character.XP = 200;
                }
                else if(character.Tier == 3)
                {
                    character.XP = 300;
                }
            }
            
            await UpdateCharacter(character);
        }
    }
}
