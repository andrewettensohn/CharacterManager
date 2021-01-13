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

        #endregion

        #region Properties

        public Character Character { get; set; }

        private bool DisplayCharacterNameInput = false;
        private string CharacterNameInputCss => DisplayCharacterNameInput ? null : "d-none";
        private string CharacterNameHeaderCss => DisplayCharacterNameInput ? "d-none" : null;
        private void ToggleCharacterNameInputDisplay() => DisplayCharacterNameInput = !DisplayCharacterNameInput;


        #endregion

        protected async override Task OnInitializedAsync()
        {
            if(Id != 0)
            {
                //Get Character
                Character = await _characterService.GetCharacter(Id);
            }
            else
            {
                //New Character
                Character = await _characterService.NewCharacter();
            }

            await base.OnInitializedAsync();
        }

        private async Task UpdateCharacterName()
        {
            ToggleCharacterNameInputDisplay();
            await _characterService.UpdateCharacter(Character);
        }

    }
}
