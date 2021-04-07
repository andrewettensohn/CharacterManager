using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Gear
    {
        public Guid GearId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public int Value { get; set; }

        public string Rarity { get; set; }

        public string Keywords { get; set; }

        public List<Character> CharacterGear { get; set; }
    }
}
