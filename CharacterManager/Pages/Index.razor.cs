using CharacterManager.DAC.Data;
using CharacterManager.Models;
using CharacterManager.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{
    public partial class Index
    {

        [Inject]
        public ICharacterRepository CharacterRepository { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        private MainMenuViewModel _vm { get; set; }

        private async Task NewCharacter()
        {
            Character newCharacter = await _vm.NewCharacter();
            NavManager.NavigateTo($"characterPage/{newCharacter.CharacterId}");
        }

        protected async override Task OnInitializedAsync()
        {
            _vm = new MainMenuViewModel();
            _vm.PropertyChanged += (sender, e) => StateHasChanged();
            _vm.InjectRepository(CharacterRepository);
            await _vm.LoadViewModel();
            await base.OnInitializedAsync();
        }
    }
}

