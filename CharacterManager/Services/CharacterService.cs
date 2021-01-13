using CharacterManager.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class CharacterService : ServiceBase
    {
        public CharacterService(ICharacterRepository characterRepository) : base(characterRepository) { }

        public async Task<List<Character>> ListCharacters() => await CharacterRepository.ListCharacters();

        public async Task<Character> GetCharacter(int id)
        {
            return await CharacterRepository.GetCharacter(id);
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                XP = 100,
                Name = "New Character",
            };

            return await CharacterRepository.NewCharacter(character);
        }
    }
}
