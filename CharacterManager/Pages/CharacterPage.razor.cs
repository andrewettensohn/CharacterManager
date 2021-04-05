using CharacterManager.DAC.Data;
using CharacterManager.ViewModels;
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
        public string Id { get; set; }

        [Inject]
        public ICharacterRepository CharacterRepository { get; set; }

        private CharacterViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            bool idIsGuid = Guid.TryParse(Id, out Guid guidId);

            if(idIsGuid)
            {
                _vm = new CharacterViewModel(guidId);
                _vm.PropertyChanged += (sender, e) => StateHasChanged();
                _vm.InjectRepository(CharacterRepository);
                await _vm.LoadViewModel();
                await base.OnInitializedAsync();
            }
        }
    }
}
