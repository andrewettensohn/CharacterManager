using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        private List<Character> _characters = new List<Character>();
        public List<Character> Characters
        {
            get => _characters;
            set
            {
                SetValue(ref _characters, value);
            }
        }

        private bool _isBusy = false;
        public bool Busy
        {
            get => _isBusy;
            set
            {
                SetValue(ref _isBusy, value);
            }
        }

        public async Task LoadViewModel()
        {
            IsBusy = true;

            Characters = await CharacterRepository.ListCharacters();
            
            foreach(Character character in Characters)
            {
                character.Archetype = await CharacterRepository.GetArchetypeForCharacter(character.CharacterId);
            }
            

            IsBusy = false;
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                Name = "New Character",
            };

            character = await CharacterRepository.NewCharacter(character);
            character.Attributes = await CharacterRepository.AddAttributes(new Attributes { CharacterId = character.CharacterId });
            character.Skills = await CharacterRepository.AddSkills(new Skills { CharacterId = character.CharacterId });

            return character;
        }
    }
}
