using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class SpellLink
    {
        public int SpellLinkId { get; set; }

        public int SpellId { get; set; }

        public int? CharacterId { get; set; }

        public int? CharacterClassId { get; set; }

    }
}
