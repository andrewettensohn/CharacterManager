using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class CombatTraits
    {
        public int CombatTraitsId { get; set; }

        public int CharacterId { get; set; }

        public int Defense { get; set; }

        public int Resilence { get; set; }

        public int Soak { get; set; }

        public int Speed { get; set; }

        public int Shock { get; set; }

        public int Wounds { get; set; }
    }
}
