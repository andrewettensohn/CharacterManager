using CharacterManager.Models;
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

        #endregion

        #region Properties

        public Character Character { get; set; }

        public List<Archetype> Archetypes { get; set; } = new List<Archetype>();

        public List<Armor> ArmorList { get; set; } = new List<Armor>();

        public List<Talent> TalentList { get; set; } = new List<Talent>();


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

        private bool Busy { get; set; }

        #endregion

        protected async override Task OnInitializedAsync()
        {
            Busy = true;

            if(Id != 0)
            {
                //Get Character
                Character = await _characterService.GetCharacter(Id);
            }

            Archetypes = await _archetypeService.ListArchetypes();
            ArmorList = await _armorService.ListArmor();
            TalentList = await _talentService.ListTalents();

            Busy = false;

            await base.OnInitializedAsync();
        }

        private async Task UpdateCharacterName()
        {
            ToggleCharacterNameInputDisplay();
            await _characterService.UpdateCharacter(Character);
        }

        private async Task UpdateTier(ChangeEventArgs args, Character character)
        {
            ToggleTierDisplay();
            int tier = int.Parse(args.Value.ToString());
            character.Tier = tier;
            await _characterService.UpdateTier(character);
        }

        private async Task UpdateXP(ChangeEventArgs args)
        {
            ToggleXPInput();
            Character.XP = int.Parse(args.Value.ToString());
            await _characterService.UpdateCharacter(Character);
        }

        private async Task UpdateArchetype()
        {
            ToggleArchetypeInputDisplay();
            if (Character.Archetype.ArchetypeId == 0)
            {
                Character = await _archetypeService.SubmitArchetype(Character);
            }
            else
            {
                Character = await _archetypeService.UpdateArchetype(Character);
            }
        }

        private async Task UpdateArmor()
        {
            ToggleArmorInputDisplay();
            if (Character.Armor.ArmorId == 0)
            {
                Character = await _armorService.SubmitArmor(Character);
            }
            else
            {
                await _armorService.UpdateArmor(Character);
            }
        }

        private async Task AddNewTalent(Talent talent)
        {
            await _talentService.AddTalent(Character, talent);
            Character.Talents.Add(talent);
        }

        private async Task RemoveTalentFromCharacter(Talent talent)
        {
            Character = await _talentService.RemoveTalentFromCharacter(Character, talent);
        }
    }
}
