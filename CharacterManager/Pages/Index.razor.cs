using CharacterManager.DAC.Data;
using CharacterManager.Models;
using CharacterManager.Models.Contracts;
using CharacterManager.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{
    public partial class Index
    {

        [Inject]
        public ICharacterManagerRepository CharacterRepository { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IWebHostEnvironment Environment { get; set; }

        private MainMenuViewModel _vm { get; set; }

        private async Task NewCharacter()
        {
            Character newCharacter = await _vm.NewCharacter();
            NavManager.NavigateTo($"characterPage/{newCharacter.Id}");
        }

        protected async override Task OnInitializedAsync()
        {
            _vm = new MainMenuViewModel();
            _vm.PropertyChanged += (sender, e) => StateHasChanged();
            _vm.InjectRepository(CharacterRepository);
            await _vm.LoadViewModel(Environment.WebRootPath);
            await base.OnInitializedAsync();
        }
    }
}

