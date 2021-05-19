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

        private PyschicPower _pyschicPower = new PyschicPower();
        public PyschicPower PyschicPower
        {
            get => _pyschicPower;
            set
            {
                SetValue(ref _pyschicPower, value);
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

        public async Task AddArchetype()
        {
            CharacterRepository.AddCoreModel(Archetype, ModelType.Archetype);
            Archetype = new Archetype();
        }

        public async Task AddTalent()
        {
            CharacterRepository.AddCoreModel(Talent, ModelType.Talent);
            Talent = new Talent();
        }

        public async Task AddWeapon()
        {
            CharacterRepository.AddCoreModel(Weapon, ModelType.Weapon);
            Weapon = new Weapon();
        }

        public async Task AddArmor()
        {
            CharacterRepository.AddCoreModel(Armor, ModelType.Armor);
            Armor = new Armor();
        }

        public async Task AddGear()
        {
            CharacterRepository.AddCoreModel(Gear, ModelType.Gear);
            Gear = new Gear();
        }

        public async Task AddPyschicPower()
        {
            CharacterRepository.AddCoreModel(PyschicPower, ModelType.Pyschic);
            PyschicPower = new PyschicPower();
        }

        public async Task GetQuestList()
        {
            QuestList = CharacterRepository.GetAllCoreModelsForModelType<Quest>(ModelType.Quest);
        }

        public async Task AddQuest()
        {
            CharacterRepository.AddCoreModel(NewQuest, ModelType.Quest);
            NewQuest = new Quest();
        }

        public async Task SetQuestToComplete(Quest quest)
        {
            quest.IsComplete = true;
            CharacterRepository.UpdateCoreModel(quest);
            await GetQuestList();
        }

        public async Task UpdateQuest(Quest quest)
        {
            CharacterRepository.UpdateCoreModel(quest);
        }
    }
}
