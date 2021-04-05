using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class WeaponLink
    {
        public Guid WeaponLinkId { get; set; }

        public Guid WeaponId { get; set; }

        public Guid CharacterId { get; set; }
    }
}
