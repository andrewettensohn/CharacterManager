using CharacterManager.Data.Contracts;
using CharacterManager.Data.Repositories;
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

        private IArchetypeRepository _archetypeRepository { get; set; }
        private IArmorRepository _armorRepository { get; set; }
        private IAttributeRepository _attributeRepository { get; set; }
        private ICharacterRepository _characterRepository { get; set; }
        private IGearRepository _gearRepository { get; set; }
        private ISkillsRepository _skillsRepository { get; set; }
        private ITalentRepository _talentRepository { get; set; }
        private IWeaponRepository _weaponRepository { get; set; }

        public override async Task LoadViewModel(IArchetypeRepository archetypeRepository, IArmorRepository armorRepository, IAttributeRepository attributeRepository, ICharacterRepository characterRepository, IGearRepository gearRepository, ISkillsRepository skillsRepository, ITalentRepository talentRepository, IWeaponRepository weaponRepository)
        {
            IsBusy = true;

            _archetypeRepository = archetypeRepository;
            _armorRepository = armorRepository;
            _attributeRepository = attributeRepository;
            _characterRepository = characterRepository;
            _gearRepository = gearRepository;
            _skillsRepository = skillsRepository;
            _talentRepository = talentRepository;
            _weaponRepository = weaponRepository;

            Characters = await _characterRepository.ListCharacters();
            
            foreach(Character character in Characters)
            {
                character.Archetype = await archetypeRepository.GetArchetypeForCharacter(character.CharacterId);
            }
            

            IsBusy = false;
        }

        public async Task<Character> NewCharacter()
        {
            Character character = new Character
            {
                Name = "New Character",
            };

            character = await _characterRepository.NewCharacter(character);
            character.Attributes = await _attributeRepository.AddAttributes(new Attributes { CharacterId = character.CharacterId });
            character.Skills = await _skillsRepository.AddSkills(new Skills { CharacterId = character.CharacterId });

            return character;
        }
    }
}
