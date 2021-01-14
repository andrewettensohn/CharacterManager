using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class ArmorLink
    {
        public int ArmorLinkId { get; set; }

        public int ArmorId { get; set; }

        public int CharacterId { get; set; }
    }
}
