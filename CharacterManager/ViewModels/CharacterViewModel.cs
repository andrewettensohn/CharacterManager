using CharacterManager.Components;
using CharacterManager.Components.CharacterDrawer;
using CharacterManager.DAC.Data;
using CharacterManager.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {

        private Character _character = new Character();
        public Character Character
        {
            get => _character;
            set
            {
                SetValue(ref _character, value);
            }
        }

        private List<Archetype> _archetypes = new List<Archetype>();
        public List<Archetype> Archetypes
        {
            get => _archetypes;
            set
            {
                SetValue(ref _archetypes, value);
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

        private List<Talent> _talentList = new List<Talent>();
        public List<Talent> TalentList
        {
            get => _talentList;
            set
            {
                SetValue(ref _talentList, value);
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

        private List<Gear> _gearList = new List<Gear>();
        public List<Gear> GearList
        {
            get => _gearList;
            set
            {
                SetValue(ref _gearList, value);
            }
        }

        private List<PyschicPower> _pyschicPowerList = new List<PyschicPower>();
        public List<PyschicPower> PyschicPowers
        {
            get => _pyschicPowerList;
            set
            {
                SetValue(ref _pyschicPowerList, value);
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

        private bool _isBusy = false;
        public bool Busy
        {
            get => _isBusy;
            set
            {
                SetValue(ref _isBusy, value);
            }
        }

        private Dictionary<string, int> _combatTraits;
        public Dictionary<string, int> CombatTraits
        {
            get => _combatTraits;
            set
            {
                SetValue(ref _combatTraits, value);
            }
        }

        private Dictionary<string, int> _skillChecks;
        public Dictionary<string, int> SkillChecks
        {
            get => _skillChecks;
            set
            {
                SetValue(ref _skillChecks, value);
            }
        }

        public DrawerComponent DrawerComponent { get; set; }

        private Guid CharacterId { get; set; }

        public CharacterViewModel(Guid Id)
        {
            CharacterId = Id;
        }

        public async Task LoadViewModel()
        {
            Character = CharacterRepository.GetCoreModelById<Character>(CharacterId);

            if (Character == null) return;

            Character.Weapons = Character.Weapons ?? new List<Weapon>();
            Character.Talents = Character.Talents ?? new List<Talent>();
            Character.CharacterGear = Character.CharacterGear ?? new List<Gear>();
            Character.PsychicPowers = Character.PsychicPowers ?? new List<PyschicPower>();

            Archetypes = CharacterRepository.GetAllCoreModelsForModelType<Archetype>(ModelType.Archetype) ?? new List<Archetype>();
            ArmorList = CharacterRepository.GetAllCoreModelsForModelType<Armor>(ModelType.Armor) ?? new List<Armor>();
            TalentList = CharacterRepository.GetAllCoreModelsForModelType<Talent>(ModelType.Talent) ?? new List<Talent>();
            WeaponList = CharacterRepository.GetAllCoreModelsForModelType<Weapon>(ModelType.Weapon) ?? new List<Weapon>();
            GearList = CharacterRepository.GetAllCoreModelsForModelType<Gear>(ModelType.Gear) ?? new List<Gear>();
            PyschicPowers = CharacterRepository.GetAllCoreModelsForModelType<PyschicPower>(ModelType.Pyschic) ?? new List<PyschicPower>();
            QuestList = CharacterRepository.GetAllCoreModelsForModelType<Quest>(ModelType.Quest) ?? new List<Quest>();

            SetCombatTraits();
            SetSkillChecks();
        }

        public void UpdateCharacter()
        {

            CharacterRepository.UpdateCoreModel(Character);

            SetCombatTraits();
            SetSkillChecks();
        }

        public async Task UpdateTier()
        {
            if (Character.Tier == 1)
            {
                Character.XP = 100;
            }
            else if (Character.Tier == 2)
            {
                Character.XP = 200;
            }
            else if (Character.Tier == 3)
            {
                Character.XP = 300;
            }

            UpdateCharacter();
        }

        public void SetCombatTraits()
        {
            CombatTraits = new Dictionary<string, int>
            {
                { "Defense", Character.Attributes.Initiative - 1 },
                { "Max Wounds", Character.Tier * 2 + Character.Attributes.Toughness},
                { "Resilience", Character.Attributes.Toughness + 1 + (Character.Armor == null ? 0 : Character.Armor.AR) },
                { "Max Shock", Character.Attributes.Willpower + Character.Tier },
                { "Determination", Character.Attributes.Toughness  },
                { "Resolve", Character.Attributes.Willpower - 1  },
                { "Passive Awareness", Character.Skills.Awareness + Character.Attributes.Intellect / 2  },
                { "Conviction", Character.Tier * 2 + Character.Attributes.Willpower},
            };
        }

        public void SetSkillChecks()
        {
            SkillChecks = new Dictionary<string, int>
            {
                { "Athletics", Character.Skills.Athletics - 1 },
                { "Awareness", Character.Skills.Awareness + Character.Attributes.Intellect},
                { "Ballistic", Character.Skills.Ballistic + Character.Attributes.Agility},
                { "Cunning", Character.Skills.Cunning + Character.Attributes.Fellowship},
                { "Deception", Character.Skills.Deception + Character.Attributes.Fellowship},
                { "Insight", Character.Skills.Insight + Character.Attributes.Fellowship},
                { "Intimidation", Character.Skills.Intimidation + Character.Attributes.Willpower},
                { "Investigation", Character.Skills.Investigation + Character.Attributes.Intellect},
                { "Leadership", Character.Skills.Leadership + Character.Attributes.Willpower},
                { "Medicae", Character.Skills.Medicae + Character.Attributes.Intellect},
                { "Persuasion", Character.Skills.Persuasion + Character.Attributes.Fellowship},
                { "Pilot", Character.Skills.Pilot + Character.Attributes.Agility},
                { "Pyschic", Character.Skills.Pyschic + Character.Attributes.Willpower},
                { "Scholar", Character.Skills.Scholar + Character.Attributes.Intellect},
                { "Stealth", Character.Skills.Stealth + Character.Attributes.Agility},
                { "Survival", Character.Skills.Survival + Character.Attributes.Willpower},
                { "Tech", Character.Skills.Survival + Character.Attributes.Intellect},
                { "Weapon", Character.Skills.Weapon + Character.Attributes.Initiative},
            };
        }

        public async Task UpdateArchetype(Archetype archetype)
        {
            if (Busy) return;
            Busy = true;

            Character.Archetype = archetype;

            if (Character.Archetype.AttributeBonus > 0)
            {
                Character.XP += Character.Archetype.AttributeBonus * 4;
            }

            if (Character.Archetype.SkillBonus > 0)
            {
                Character.XP += Character.Archetype.SkillBonus * 2;
            }

            Character.XP -= Character.Archetype.XPCost;

            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task UpdateAttribute(string attributeName, bool isIncrease)
        {

            int value = int.Parse(Character.Attributes.GetType().GetProperty(attributeName).GetValue(Character.Attributes).ToString());

            if (isIncrease)
            {
                //decrease XP
                Character.XP -= 4;
                value += 1;
            }
            else
            {
                //increase XP
                Character.XP += 4;
                value -= 1;
            }

            Character.Attributes.GetType().GetProperty(attributeName).SetValue(Character.Attributes, value);
            UpdateCharacter();
        }

        public async Task UpdateSkill(string skillName, bool isIncrease)
        {

            int value = int.Parse(Character.Skills.GetType().GetProperty(skillName).GetValue(Character.Skills).ToString());

            if (isIncrease)
            {
                //decrease XP
                Character.XP -= 2;
                value += 1;
            }
            else
            {
                //increase XP
                Character.XP += 2;
                value -= 1;
            }

            Character.Skills.GetType().GetProperty(skillName).SetValue(Character.Skills, value);
            UpdateCharacter();
        }

        public async Task UpdateArmor(Armor armor, bool isEquipped)
        {
            if (Busy) return;
            Busy = true;

            armor.IsEquipped = isEquipped;
            Character.Armor = armor;
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));
            SetCombatTraits();

            Busy = false;
        }

        public async Task RemoveArmor()
        {
            if (Busy) return;
            Busy = true;

            Character.Armor = null;
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));
            SetCombatTraits();

            Busy = false;
        }


        #region Talent

        public async Task RemoveTalentFromCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character.XP += talent.XPCost;
            Character.Talents.RemoveAll(x => x.Name == talent.Name);
            UpdateCharacter();

            OnPropertyChanged(nameof(TalentList));

            Busy = false;
        }

        public async Task AddExistingTalentToCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character.XP -= talent.XPCost;
            Character.Talents.Add(talent);
            UpdateCharacter();

            OnPropertyChanged(nameof(TalentList));

            Busy = false;
        }


        #endregion

        #region Gear

        public async Task RemoveGearFromCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            Character.CharacterGear.RemoveAll(x => x.Name == gear.Name);
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task AddExistingGearToCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            Character.CharacterGear.Add(gear);
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        #endregion

        #region Pyschic

        public async Task RemovePyschicFromCharacter(PyschicPower psychicPower)
        {
            if (Busy) return;
            Busy = true;

            Character.PsychicPowers.RemoveAll(x => x.Name == psychicPower.Name);
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task AddExistingPyschicToCharacter(PyschicPower psychicPower)
        {
            if (Busy) return;
            Busy = true;

            Character.PsychicPowers.Add(psychicPower);
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        #endregion

        #region Weapon

        public async Task RemoveWeaponFromCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            Character.Weapons.RemoveAll(x => x.Name == weapon.Name);
            UpdateCharacter();

            OnPropertyChanged(nameof(WeaponList));

            Busy = false;
        }

        public async Task UpdateWeaponStatus(Weapon weapon, bool isEquipped)
        {
            if (Busy) return;
            Busy = true;

            weapon.IsEquipped = isEquipped;
            UpdateCharacter();

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task AddExistingWeaponToCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            Character.Weapons.Add(weapon);
            UpdateCharacter();

            OnPropertyChanged(nameof(WeaponList));

            Busy = false;
        }


        #endregion

    }
}
