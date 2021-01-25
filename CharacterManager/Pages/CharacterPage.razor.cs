﻿using CharacterManager.Models;
using CharacterManager.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{

    public partial class CharacterPage
    {
        [Parameter]
        public int Id { get; set; }

        #region Services

        [Inject]
        private CharacterService _characterService { get; set; }
        
        [Inject]
        private ArchetypeService _archetypeService { get; set; }

        [Inject]
        private ArmorService _armorService { get; set; }

        [Inject]
        private TalentService _talentService { get; set; }

        [Inject]
        private WeaponService _weaponService { get; set; }

        [Inject]
        private GearService _gearService { get; set; }

        #endregion

        #region Properties

        public Character Character { get; set; }

        public List<Archetype> Archetypes { get; set; } = new List<Archetype>();

        public List<Armor> ArmorList { get; set; } = new List<Armor>();

        public List<Talent> TalentList { get; set; } = new List<Talent>();

        public List<Weapon> WeaponList { get; set; } = new List<Weapon>();

        public List<Gear> GearList { get; set; } = new List<Gear>();


        private bool DisplayCharacterNameInput = false;
        private string CharacterNameInputCss => DisplayCharacterNameInput ? null : "d-none";
        private string CharacterNameHeaderCss => DisplayCharacterNameInput ? "d-none" : null;
        private void ToggleCharacterNameInputDisplay() => DisplayCharacterNameInput = !DisplayCharacterNameInput;

        private bool DisplayTierInput = false;
        private string TierInputCss => DisplayTierInput ? null : "d-none";
        private string TierHeaderCss => DisplayTierInput ? "d-none" : null;
        private void ToggleTierDisplay() => DisplayTierInput = !DisplayTierInput;


        private bool DisplayXPInput = false;
        private string XPInputCss => DisplayXPInput ? null : "d-none";
        private string XPHeaderCss => DisplayXPInput ? "d-none" : null;
        private void ToggleXPInput() => DisplayXPInput = !DisplayXPInput;


        private bool DisplayArchetypeInput = false;
        private string ArchetypeInputCss => DisplayArchetypeInput ? null : "d-none";
        private string ArchetypeInfoCss => DisplayArchetypeInput ? "d-none" : null;
        private void ToggleArchetypeInputDisplay() => DisplayArchetypeInput = !DisplayArchetypeInput;


        private bool DisplayArmorInput = false;
        private string ArmorInputCss => DisplayArmorInput ? null : "d-none";
        private string ArmorInfoCss => DisplayArmorInput ? "d-none" : null;
        private void ToggleArmorInputDisplay() => DisplayArmorInput = !DisplayArmorInput;


        private bool DisplayTalentInput = false;
        private string TalentInputCss => DisplayTalentInput ? null : "d-none";
        private string TalentInfoCss => DisplayTalentInput ? "d-none" : null;
        private void ToggleTalentInputDisplay() => DisplayTalentInput = !DisplayTalentInput;


        private bool DisplayWeaponInput = false;
        private string WeaponInputCss => DisplayWeaponInput ? null : "d-none";
        private string WeaponInfoCss => DisplayWeaponInput ? "d-none" : null;
        private void ToggleWeaponInputDisplay() => DisplayWeaponInput = !DisplayWeaponInput;


        private bool DisplayGearInput = false;
        private string GearInputCss => DisplayGearInput ? null : "d-none";
        private string GearInfoCss => DisplayGearInput ? "d-none" : null;
        private void ToggleGearInputDisplay() => DisplayGearInput = !DisplayGearInput;


        private bool Busy { get; set; }

        #endregion

        protected async override Task OnInitializedAsync()
        {
            if (Busy == true) return;
            Busy = true;

            if(Id != 0)
            {
                //Get Character
                Character = await _characterService.GetCharacter(Id);
            }

            Archetypes = await _archetypeService.ListArchetypes();
            ArmorList = await _armorService.ListArmor();
            TalentList = await _talentService.ListTalents();
            WeaponList = await _weaponService.ListWeapons();
            GearList = await _gearService.ListGear();

            Busy = false;

            await base.OnInitializedAsync();
        }

        private async Task UpdateCharacterName()
        {
            if (Busy) return;
            Busy = true;

            ToggleCharacterNameInputDisplay();
            await _characterService.UpdateCharacter(Character);

            Busy = false;
        }

        private async Task UpdateTier(ChangeEventArgs args, Character character)
        {
            if (Busy) return;
            Busy = true;

            ToggleTierDisplay();
            int tier = int.Parse(args.Value.ToString());
            character.Tier = tier;
            await _characterService.UpdateTier(character);

            Busy = false;
        }

        private async Task UpdateXP(ChangeEventArgs args)
        {
            if (Busy) return;
            Busy = true;

            ToggleXPInput();
            Character.XP = int.Parse(args.Value.ToString());
            await _characterService.UpdateCharacter(Character);

            Busy = false;
        }

        private async Task UpdateArchetype()
        {
            if (Busy) return;
            Busy = true;

            ToggleArchetypeInputDisplay();
            if (Character.Archetype.ArchetypeId == 0)
            {
                Character = await _archetypeService.SubmitArchetype(Character);
            }
            else
            {
                Character = await _archetypeService.UpdateArchetype(Character);
            }

            Busy = false;
        }

        private async Task UpdateArmor()
        {
            if (Busy) return;
            Busy = true;

            ToggleArmorInputDisplay();
            if (Character.Armor.ArmorId == 0)
            {
                Character = await _armorService.SubmitArmor(Character);
            }
            else
            {
                await _armorService.UpdateArmor(Character);
            }

            Busy = false;
        }

        private async Task UpdateAttributeOrSkill()
        {
            if (Busy) return;
            Busy = true;

            await _characterService.UpdateCharacter(Character);

            Busy = false;
        }

        #region Talent

        private async Task AddNewTalent(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            await _talentService.AddTalent(Character, talent);
            Character.Talents.Add(talent);

            Busy = false;
        }

        private async Task RemoveTalentFromCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            Character = await _talentService.RemoveTalentFromCharacter(Character, talent);

            Busy = false;
        }

        private async Task UpdateTalents()
        {
            if (Busy) return;
            Busy = true;

            await _talentService.UpdateTalents(Character.Talents);

            Busy = false;
        }

        private async Task AddExistingTalentToCharacter(Talent talent)
        {
            if (Busy) return;
            Busy = true;

            await _talentService.AddExistingTalentToCharacter(Character, talent as Talent);

            Busy = false;
        }


        #endregion

        #region Gear

        private async Task UpdateGearList()
        {
            if (Busy) return;
            Busy = true;

            await _gearService.UpdateGearList(Character.Gear);

            Busy = false;
        }

        private async Task AddGear(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearService.AddGear(Character, gear);

            Busy = false;
        }

        private async Task RemoveGearFromCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearService.RemoveGearFromCharacter(Character, gear);

            Busy = false;
        }

        private async Task AddExistingGearToCharacter(Gear gear)
        {
            if (Busy) return;
            Busy = true;

            await _gearService.AddExistingGearToCharacter(Character, gear);

            Busy = false;
        }

        #endregion


        #region Weapon

        private async Task UpdateWeapons()
        {
            if (Busy) return;
            Busy = true;

            await _weaponService.UpdateWeapons(Character.Weapons);

            Busy = false;
        }

        private async Task AddWeapon(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponService.AddWeapon(Character, weapon);

            Busy = false;
        }

        private async Task RemoveWeaponFromCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponService.RemoveWeaponFromCharacter(Character, weapon);

            Busy = false;
        }

        private async Task AddExistingWeaponToCharacter(Weapon weapon)
        {
            if (Busy) return;
            Busy = true;

            await _weaponService.AddExistingWeaponToCharacter(Character, weapon);

            Busy = false;
        }


        #endregion



    }
}
