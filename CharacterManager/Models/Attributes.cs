using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Attributes
    {
        public int AttributesId { get; set; }

        public int CharacterId { get; set; }

        public int Strength { get; set; }

        public int Agility { get; set; }

        public int Toughness { get; set; }

        public int Intellect { get; set; }

        public int Willpower { get; set; }

        public int Fellowship { get; set; }

        public int Initiative { get; set; }
    }
}
