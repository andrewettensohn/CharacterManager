using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        private string[] _avatarFileNames;
        public string[] AvatarFileNames
        {
            get => _avatarFileNames;
            set
            {
                SetValue(ref _avatarFileNames, value);
            }
        }

        public async Task LoadViewModel(string webRootPath)
        {
            IsBusy = true;

            Characters = CharacterRepository.GetAllCoreModelsForModelType<Character>(ModelType.Character);

            AvatarFileNames = Directory.GetFiles($"{webRootPath}\\art").Select(Path.GetFileName).ToArray();

            IsBusy = false;
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                Id = Guid.NewGuid(),
                Name = "New Character",
                Skills = new Skills(),
                Attributes = new Attributes(),
            };

            character = CharacterRepository.AddCoreModel(character, ModelType.Character);

            return character;
        }

        public async Task UpdateCharacterAvatar(Character character, string path)
        {
            character.AvatarPath = path;
            CharacterRepository.UpdateCoreModel(character);

            Characters = CharacterRepository.GetAllCoreModelsForModelType<Character>(ModelType.Character);
        }
    }
}
