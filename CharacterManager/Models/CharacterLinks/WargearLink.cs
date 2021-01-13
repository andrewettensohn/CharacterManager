using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class WargearLink
    {
        public int WargearLinkId { get; set; }

        public int CharacterId { get; set; }

        public int WargearId { get; set; }
    }
}
