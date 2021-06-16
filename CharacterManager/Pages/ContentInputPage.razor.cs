using CharacterManager.DAC.Data;
using CharacterManager.Models.Contracts;
using CharacterManager.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{
    public partial class ContentInputPage
    {
        [Inject]
        public ICharacterManagerRepository CharacterRepository { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        private ContentInputViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ContentInputViewModel(Snackbar, CharacterRepository);
            _vm.PropertyChanged += (sender, e) => StateHasChanged();
            _vm.InjectRepository(CharacterRepository);
            await base.OnInitializedAsync();
        }
    }
}
