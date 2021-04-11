using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Gear
    {
        [JsonIgnore]
        public Guid GearId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public int Value { get; set; }

        public string Rarity { get; set; }

        public string Keywords { get; set; }

        [JsonIgnore]
        public List<Character> CharacterGear { get; set; }
    }
}
