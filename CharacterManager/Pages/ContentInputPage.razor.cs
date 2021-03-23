using CharacterManager.Data.Contracts;
using CharacterManager.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Pages
{
    public partial class ContentInputPage
    {
        [Inject]
        public IArchetypeRepository ArchetypeRepository { get; set; }
        [Inject]
        public IArmorRepository ArmorRepository { get; set; }
        [Inject]
        public IAttributeRepository AttributeRepository { get; set; }
        [Inject]
        public ICharacterRepository CharacterRepository { get; set; }
        [Inject]
        public IGearRepository GearRepository { get; set; }
        [Inject]
        public ISkillsRepository SkillsRepository { get; set; }
        [Inject]
        public ITalentRepository TalentRepository { get; set; }
        [Inject]
        public IWeaponRepository WeaponRepository { get; set; }

        private ContentInputViewModel _vm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _vm = new ContentInputViewModel();
            _vm.PropertyChanged += (sender, e) => StateHasChanged();
            await _vm.LoadViewModel(ArchetypeRepository, ArmorRepository, AttributeRepository, CharacterRepository, GearRepository, SkillsRepository, TalentRepository, WeaponRepository);
            await base.OnInitializedAsync();
        }
    }
}
