using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class WeaponLink
    {
        public int WeaponLinkId { get; set; }

        public int WeaponId { get; set; }

        public int CharacterId { get; set; }
    }
}
