using CharacterManager.Data.Contracts;
using CharacterManager.Data.Repositories;
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

        private string NoCssClass = string.Empty;
        private string NoDisplayCssClass = "d-none";

        private bool DisplayCharacterNameInput = false;
        public string CharacterNameInputCss => DisplayCharacterNameInput ? NoCssClass : NoDisplayCssClass;
        public string CharacterNameHeaderCss => DisplayCharacterNameInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleCharacterNameInputDisplay() => DisplayCharacterNameInput = !DisplayCharacterNameInput;

        private bool DisplayTierInput = false;
        public string TierInputCss => DisplayTierInput ? NoCssClass : NoDisplayCssClass;
        public string TierHeaderCss => DisplayTierInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleTierDisplay() => DisplayTierInput = !DisplayTierInput;


        private bool DisplayXPInput = false;
        public string XPInputCss => DisplayXPInput ? NoCssClass : NoDisplayCssClass;
        public string XPHeaderCss => DisplayXPInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleXPInput() => DisplayXPInput = !DisplayXPInput;


        private bool DisplayArchetypeInput = false;
        public string ArchetypeInputCss => DisplayArchetypeInput ? NoCssClass : NoDisplayCssClass;
        public string ArchetypeInfoCss => DisplayArchetypeInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleArchetypeInputDisplay() => DisplayArchetypeInput = !DisplayArchetypeInput;


        private bool DisplayArmorInput = false;
        public string ArmorInputCss => DisplayArmorInput ? NoCssClass : NoDisplayCssClass;
        public string ArmorInfoCss => DisplayArmorInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleArmorInputDisplay() => DisplayArmorInput = !DisplayArmorInput;


        private bool DisplayTalentInput = false;
        public string TalentInputCss => DisplayTalentInput ? NoCssClass : NoDisplayCssClass;
        public string TalentInfoCss => DisplayTalentInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleTalentInputDisplay() => DisplayTalentInput = !DisplayTalentInput;


        private bool DisplayWeaponInput = false;
        public string WeaponInputCss => DisplayWeaponInput ? NoCssClass : NoDisplayCssClass;
        public string WeaponInfoCss => DisplayWeaponInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleWeaponInputDisplay() => DisplayWeaponInput = !DisplayWeaponInput;


        private bool DisplayGearInput = false;
        public string GearInputCss => DisplayGearInput ? NoCssClass : NoDisplayCssClass;
        public string GearInfoCss => DisplayGearInput ? NoDisplayCssClass : NoCssClass;
        public void ToggleGearInputDisplay() => DisplayGearInput = !DisplayGearInput;

        private int CharacterId { get; set; }

        private IArchetypeRepository _archetypeRepository { get; set; }
        private IArmorRepository _armorRepository { get; set; }
        private IAttributeRepository _attributeRepository { get; set; }
        private ICharacterRepository _characterRepository { get; set; }
        private IGearRepository _gearRepository { get; set; }
        private ISkillsRepository _skillsRepository { get; set; }
        private ITalentRepository _talentRepository { get; set; }
        private IWeaponRepository _weaponRepository { get; set; }

        public CharacterViewModel(int Id)
        {
            CharacterId = Id;
        }

        public override async Task LoadViewModel(
            IArchetypeRepository archetypeRepository, 
            IArmorRepository armorRepository, 
            IAttributeRepository attributeRepository, 
            ICharacterRepository characterRepository, 
            IGearRepository gearRepository,
            ISkillsRepository skillsRepository,
            ITalentRepository talentRepository,
            IWeaponRepository weaponRepository)
        {
            _archetypeRepository = archetypeRepository;
            _armorRepository = armorRepository;
            _attributeRepository = attributeRepository;
            _characterRepository = characterRepository;
            _gearRepository = gearRepository;
            _skillsRepository = skillsRepository;
            _talentRepository = talentRepository;
            _weaponRepository = weaponRepository;


            Character = await _characterRepository.GetCharacter(CharacterId);

            if (Character == null) return;

            Archetypes = await _archetypeRepository.GetArchetypes();
            ArmorList = await _armorRepository.GetArmorList();
            TalentList = await _talentRepository.GetTalents();
            WeaponList = await _weaponRepository.GetWeapons();
            GearList = await _gearRepository.GetGearList();
        }

        public async Task UpdateCharacterName()
        {
            if (Busy) return;
            Busy = true;

            ToggleCharacterNameInputDisplay();
            await _characterRepository.UpdateCharacter(Character);

            Busy = false;
        }

        public async Task UpdateTier(ChangeEventArgs args, Character character)
        {
            if (Busy) return;
            Busy = true;

            ToggleTierDisplay();
            int tier = int.Parse(args.Value.ToString());
            character.Tier = tier;
            await _characterRepository.UpdateCharacter(character);

            Busy = false;
        }

        public async Task UpdateXP(ChangeEventArgs args)
        {
            if (Busy) return;
            Busy = true;

            ToggleXPInput();
            Character.XP = int.Parse(args.Value.ToString());

            if (Character.XP == 0)
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
            }

            await _characterRepository.UpdateCharacter(Character);

            Busy = false;
        }

        public async Task UpdateArchetype()
        {
            if (Busy) return;
            Busy = true;

            ToggleArchetypeInputDisplay();
            if (Character.Archetype.ArchetypeId == 0)
            {
                Character = await _archetypeRepository.AddArchetype(Character);
            }
            else
            {
                Character = await _archetypeRepository.UpdateArchetype(Character);
            }

            await _characterRepository.UpdateCharacter(Character);

            Busy = false;
        }

        public async Task UpdateArmor()
        {
            if (Busy) return;
            Busy = true;

            ToggleArmorInputDisplay();
            if (Character.Armor.ArmorId == 0)
            {
                Character.Armor = await _armorRepository.AddArmor(Character);
            }
            else
            {
                await _armorRepository.UpdateArmor(Character);
            }

            Busy = false;
        }

        public async Task UpdateAttributeOrSkill()
        {
            if (Busy) return;
            Busy = true;

            await _characterRepository.UpdateCharacter(Character);

            Busy = false;
        }

        #region Talent

        public async Task AddNewTalent(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            await _talentRepository.AddTalent(Character, talent);
            Character.Talents.Add(talent);

            Busy = false;
        }

        public async Task RemoveTalentFromCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character = await _talentRepository.RemoveTalentFromCharacter(Character, talent);

            Busy = false;
        }

        public async Task UpdateTalents()
        {
            if (Busy) return;
            Busy = true;

            await _talentRepository.UpdateTalents(Character.Talents);

            Busy = false;
        }

        public async Task AddExistingTalentToCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            await _talentRepository.AddExistingTalentToCharacter(Character, talent as Talent);

            Busy = false;
        }


        #endregion

        #region Gear

        public async Task UpdateGearList()
        {
            if (Busy) return;
            Busy = true;

            await _gearRepository.UpdateGearList(Character.Gear);

            Busy = false;
        }

        public async Task AddGear(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearRepository.AddGear(Character, gear);

            Busy = false;
        }

        public async Task RemoveGearFromCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearRepository.RemoveGearFromCharacter(Character, gear);

            Busy = false;
        }

        public async Task AddExistingGearToCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearRepository.AddExistingGearToCharacter(Character, gear);

            Busy = false;
        }

        #endregion


        #region Weapon

        public async Task UpdateWeapons()
        {
            if (Busy) return;
            Busy = true;

            await _weaponRepository.UpdateWeapons(Character.Weapons);

            Busy = false;
        }

        public async Task AddWeapon(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponRepository.AddWeapon(Character, weapon);

            Busy = false;
        }

        public async Task RemoveWeaponFromCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponRepository.RemoveWeaponFromCharacter(Character, weapon);

            Busy = false;
        }

        public async Task AddExistingWeaponToCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponRepository.AddExistingWeaponToCharacter(Character, weapon);

            Busy = false;
        }


        #endregion

    }
}
