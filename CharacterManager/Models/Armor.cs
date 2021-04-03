using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Armor
    {
        public Guid ArmorId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AR { get; set; }

        public string Traits { get; set; }

        public string Value { get; set; }

        public string Keywords { get; set; }

        public bool IsEquipped { get; set; }
    }
}
