using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class MentalTraits
    {
        public int MentalTraitsId { get; set; }

        public int CharacterId { get; set; }

        public int Conviction { get; set; }

        public int Corruption { get; set; }

        public int PassiveAwareness { get; set; }

        public int Resolve { get; set; }
    }
}
