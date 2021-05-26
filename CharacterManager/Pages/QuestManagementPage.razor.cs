using CharacterManager.DAC.Data;
using CharacterManager.Models.Contracts;
using CharacterManager.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{
    public partial class QuestManagementPage
    {
        [Inject]
        public ICharacterManagerRepository CharacterRepository { get; set; }

        private ContentInputViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ContentInputViewModel();
            _vm.PropertyChanged += (sender, e) => StateHasChanged();
            _vm.InjectRepository(CharacterRepository);
            await base.OnInitializedAsync();
        }
    }
}
