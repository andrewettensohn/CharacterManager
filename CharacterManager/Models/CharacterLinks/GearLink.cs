using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class GearLink
    {
        public int GearLinkId { get; set; }

        public int CharacterId { get; set; }

        public int GearId { get; set; }
    }
}
