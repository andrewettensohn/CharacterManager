using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels
{
    public class ContentInputViewModel : BaseViewModel
    {

        private Archetype _archetype = new Archetype();
        public Archetype Archetype
        {
            get => _archetype;
            set
            {
                SetValue(ref _archetype, value);
            }
        }

        private Talent _talent = new Talent();
        public Talent Talent
        {
            get => _talent;
            set
            {
                SetValue(ref _talent, value);
            }
        }

        private Weapon _weapon = new Weapon();
        public Weapon Weapon
        {
            get => _weapon;
            set
            {
                SetValue(ref _weapon, value);
            }
        }

        private Armor _armor = new Armor();
        public Armor Armor
        {
            get => _armor;
            set
            {
                SetValue(ref _armor, value);
            }
        }

        private Gear _gear = new Gear();
        public Gear Gear
        {
            get => _gear;
            set
            {
                SetValue(ref _gear, value);
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
            _archetypeRepository = archetypeRepository;
            _armorRepository = armorRepository;
            _attributeRepository = attributeRepository;
            _characterRepository = characterRepository;
            _gearRepository = gearRepository;
            _skillsRepository = skillsRepository;
            _talentRepository = talentRepository;
            _weaponRepository = weaponRepository;
        }

        public async Task AddArchetype()
        {
            await _archetypeRepository.AddNewArchetype(Archetype);
            Archetype = new Archetype();
        }

        public async Task AddTalent()
        {
            await _talentRepository.AddNewTalent(Talent);
            Talent = new Talent();
        }

        public async Task AddWeapon()
        {
            await _weaponRepository.AddNewWeapon(Weapon);
            Weapon = new Weapon();
        }

        public async Task AddArmor()
        {
            await _armorRepository.AddNewArmor(Armor);
            Armor = new Armor();
        }

        public async Task AddGear()
        {
            await _gearRepository.AddNewGear(Gear);
            Gear = new Gear();
        }
    }
}
