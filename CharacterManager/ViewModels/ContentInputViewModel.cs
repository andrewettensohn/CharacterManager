using CharacterManager.Models;
using CharacterManager.Models.Contracts;
using MudBlazor;
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

        private List<Archetype> _archetypeList = new List<Archetype>();
        public List<Archetype> ArchetypeList
        {
            get => _archetypeList;
            set
            {
                SetValue(ref _archetypeList, value);
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

        private List<Talent> _talentList = new List<Talent>();
        public List<Talent> TalentList
        {
            get => _talentList;
            set
            {
                SetValue(ref _talentList, value);
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

        private List<Weapon> _weaponList = new List<Weapon>();
        public List<Weapon> WeaponList
        {
            get => _weaponList;
            set
            {
                SetValue(ref _weaponList, value);
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


        private List<Armor> _armorList = new List<Armor>();
        public List<Armor> ArmorList
        {
            get => _armorList;
            set
            {
                SetValue(ref _armorList, value);
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

        private List<Gear> _gearList = new List<Gear>();
        public List<Gear> GearList
        {
            get => _gearList;
            set
            {
                SetValue(ref _gearList, value);
            }
        }

        private PyschicPower _pyschicPower = new PyschicPower();
        public PyschicPower PyschicPower
        {
            get => _pyschicPower;
            set
            {
                SetValue(ref _pyschicPower, value);
            }
        }

        private List<PyschicPower> _pyschicPowerList = new List<PyschicPower>();
        public List<PyschicPower> PyschicPowerList
        {
            get => _pyschicPowerList;
            set
            {
                SetValue(ref _pyschicPowerList, value);
            }
        }

        private Quest _newQuest = new Quest();
        public Quest NewQuest
        {
            get => _newQuest;
            set
            {
                SetValue(ref _newQuest, value);
            }
        }

        private List<Quest> _questList = new List<Quest>();
        public List<Quest> QuestList
        {
            get => _questList;
            set
            {
                SetValue(ref _questList, value);
            }
        }

        public ISnackbar SnackBar { get; set; }
        private const string _saveSuccessMessage = "Saved.";

        public ContentInputViewModel(ISnackbar snackbar, ICharacterManagerRepository characterRepository)
        {
            SnackBar = snackbar;

            CharacterRepository = characterRepository;

            ArmorList = CharacterRepository.GetAllCoreModelsForModelType<Armor>(ModelType.Armor) ?? new List<Armor>();
            TalentList = CharacterRepository.GetAllCoreModelsForModelType<Talent>(ModelType.Talent) ?? new List<Talent>();
            WeaponList = CharacterRepository.GetAllCoreModelsForModelType<Weapon>(ModelType.Weapon) ?? new List<Weapon>();
            ArchetypeList = CharacterRepository.GetAllCoreModelsForModelType<Archetype>(ModelType.Archetype) ?? new List<Archetype>();
            GearList = CharacterRepository.GetAllCoreModelsForModelType<Gear>(ModelType.Gear) ?? new List<Gear>();
            PyschicPowerList = CharacterRepository.GetAllCoreModelsForModelType<PyschicPower>(ModelType.Pyschic) ?? new List<PyschicPower>();
            QuestList = CharacterRepository.GetAllCoreModelsForModelType<Quest>(ModelType.Quest) ?? new List<Quest>();
        }

        public async Task AddArchetype()
        {
            Archetype.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(Archetype, ModelType.Archetype);
            Archetype = new Archetype();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdateArchetype(Archetype archetype)
        {
            CharacterRepository.UpdateCoreModel(archetype);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddTalent()
        {
            Talent.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(Talent, ModelType.Talent);
            Talent = new Talent();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdateTalent(Talent talent)
        {
            CharacterRepository.UpdateCoreModel(talent);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddWeapon()
        {
            Weapon.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(Weapon, ModelType.Weapon);
            Weapon = new Weapon();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdateWeapon(Weapon weapon)
        {
            CharacterRepository.UpdateCoreModel(weapon);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddArmor()
        {
            Armor.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(Armor, ModelType.Armor);
            Armor = new Armor();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdateArmor(Armor armor)
        {
            CharacterRepository.UpdateCoreModel(armor);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddGear()
        {
            Gear.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(Gear, ModelType.Gear);
            Gear = new Gear();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdateGear(Gear gear)
        {
            CharacterRepository.UpdateCoreModel(gear);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddPyschicPower()
        {
            PyschicPower.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(PyschicPower, ModelType.Pyschic);
            PyschicPower = new PyschicPower();
            SnackBar.Add(_saveSuccessMessage);
        }

        public void UpdatePyschicPower(PyschicPower power)
        {
            CharacterRepository.UpdateCoreModel(power);
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task AddQuest()
        {
            NewQuest.Id = Guid.NewGuid();
            CharacterRepository.AddCoreModel(NewQuest, ModelType.Quest);
            NewQuest = new Quest();
            SnackBar.Add(_saveSuccessMessage);
        }

        public async Task UpdateQuest(Quest quest)
        {
            CharacterRepository.UpdateCoreModel(quest);
            SnackBar.Add(_saveSuccessMessage);
        }
    }
}
