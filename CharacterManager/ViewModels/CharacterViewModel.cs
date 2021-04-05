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


        private Guid CharacterId { get; set; }

        public CharacterViewModel(Guid Id)
        {
            CharacterId = Id;
        }

        public async Task LoadViewModel()
        {


            Character = await CharacterRepository.GetCharacter(CharacterId);

            Character.Archetype = await CharacterRepository.GetArchetypeForCharacter(CharacterId) ?? new Archetype();
            Character.Armor = await CharacterRepository.GetArmorForCharacter(CharacterId) ?? new Armor();
            Character.Talents = await CharacterRepository.GetTalentsForCharacter(CharacterId) ?? new List<Talent>();
            Character.Weapons = await CharacterRepository.GetWeaponsForCharacter(CharacterId) ?? new List<Weapon>();
            Character.Gear = await CharacterRepository.GetGearListForCharacter(CharacterId) ?? new List<Gear>();

            if (Character == null) return;

            Character.Attributes = await CharacterRepository.GetCharacterAttributes(CharacterId) ?? new Attributes();
            Character.Skills = await CharacterRepository.GetCharacterSkills(CharacterId) ?? new Skills();

            Archetypes = await CharacterRepository.GetArchetypes() ?? new List<Archetype>();
            ArmorList = await CharacterRepository.GetArmorList() ?? new List<Armor>();
            TalentList = await CharacterRepository.GetTalents() ?? new List<Talent>();
            WeaponList = await CharacterRepository.GetWeapons() ?? new List<Weapon>();
            GearList = await CharacterRepository.GetGearList() ?? new List<Gear>();

            SetCombatTraits();
            SetSkillChecks();
        }

        public async Task UpdateCharacter()
        {
            if (Busy) return;
            Busy = true;

            await CharacterRepository.UpdateCharacter(Character);
            Character = await CharacterRepository.GetCharacter(CharacterId);

            Busy = false;
        }
        
        public void SetCombatTraits()
        {
            CombatTraits = new Dictionary<string, int>
            {
                { "Defense", Character.Attributes.Initiative - 1 },
                { "Max Wounds", Character.Tier * 2 + Character.Attributes.Toughness},
                { "Resilience", Character.Attributes.Toughness + 1 + Character.Armor.AR },
                { "Shock", Character.Attributes.Willpower + Character.Tier },
                { "Determination", Character.Attributes.Toughness  },
                { "Resolve", Character.Attributes.Willpower - 1  },
                { "Passive Awareness", Character.Skills.Awareness + Character.Attributes.Intellect / 2  },
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
            await CharacterRepository.UpdateArchetype(Character);

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
            await UpdateCharacter();
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
            await UpdateCharacter();
        }

        public async Task UpdateArmor(Armor armor, bool isEquipped)
        {
            if (Busy) return;
            Busy = true;

            armor.IsEquipped = isEquipped;
            Character.Armor = armor;
            await CharacterRepository.UpdateArmor(Character);

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }


        #region Talent

        public async Task RemoveTalentFromCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character.XP += talent.XPCost;
            await CharacterRepository.UpdateCharacter(Character);
            await CharacterRepository.RemoveTalentFromCharacter(Character, talent);

            Character.Talents.Remove(talent);
            OnPropertyChanged(nameof(TalentList));

            Busy = false;
        }

        public async Task AddExistingTalentToCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character.XP -= talent.XPCost;
            await CharacterRepository.UpdateCharacter(Character);
            await CharacterRepository.AddExistingTalentToCharacter(Character, talent);

            Character.Talents.Add(talent);
            OnPropertyChanged(nameof(TalentList));

            Busy = false;
        }


        #endregion

        #region Gear

        public async Task RemoveGearFromCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await CharacterRepository.RemoveGearFromCharacter(Character, gear);

            Character.Gear.Remove(gear);
            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task AddExistingGearToCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await CharacterRepository.AddExistingGearToCharacter(Character, gear);

            Character.Gear.Add(gear);
            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        #endregion


        #region Weapon

        public async Task RemoveWeaponFromCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await CharacterRepository.RemoveWeaponFromCharacter(Character, weapon);

            Character.Weapons.Remove(weapon);
            OnPropertyChanged(nameof(WeaponList));

            Busy = false;
        }

        public async Task UpdateWeaponStatus(Weapon weapon, bool isEquipped)
        {
            if (Busy) return;
            Busy = true;

            weapon.IsEquipped = isEquipped;

            await CharacterRepository.UpdateWeapon(weapon);

            OnPropertyChanged(nameof(Character));

            Busy = false;
        }

        public async Task AddExistingWeaponToCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await CharacterRepository.AddExistingWeaponToCharacter(Character, weapon);

            Character.Weapons.Add(weapon);
            OnPropertyChanged(nameof(WeaponList));

            Busy = false;
        }


        #endregion

    }
}
