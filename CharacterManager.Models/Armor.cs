using CharacterManager.DAC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Armor : ICoreCharacterModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AR { get; set; }

        public string Traits { get; set; }

        public string Value { get; set; }

        public string Keywords { get; set; }

        public bool IsEquipped { get; set; }
    }
}
