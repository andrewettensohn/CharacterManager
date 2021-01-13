﻿using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterService : ServiceBase
    {
        public CharacterService(ICharacterRepository characterRepository, IAttributeRepository attributeRepository, ISkillsRepository skillsRepository) 
            : base(characterRepository, attributeRepository, skillsRepository) { }

        public async Task<List<Character>> ListCharacters() => await CharacterRepository.ListCharacters();

        public async Task<Character> GetCharacter(int id)
        {
            Character character = await CharacterRepository.GetCharacter(id);
            character.Attributes = await AttributesRepository.GetCharacterAttributes(id);
            character.Skills = await SkillsRepository.GetCharacterSkills(id);

            return character;
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                XP = 100,
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
    }
}
