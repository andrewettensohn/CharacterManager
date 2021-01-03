using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class RaceLink
    {
        public int RaceLinkId { get; set; }

        public int CharacterId { get; set; }

        public int RaceId { get; set; }
    }
}
