using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class GearLink
    {
        public Guid GearLinkId { get; set; }

        public Guid CharacterId { get; set; }

        public Guid GearId { get; set; }
    }
}
