using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class ArmorLink
    {
        public Guid ArmorLinkId { get; set; }

        public Guid ArmorId { get; set; }

        public Guid CharacterId { get; set; }
    }
}
