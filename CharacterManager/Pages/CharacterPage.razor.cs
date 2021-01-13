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

        private Character Character { get; set; }

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

    }
}
