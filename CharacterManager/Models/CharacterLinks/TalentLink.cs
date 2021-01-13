using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class TalentLink
    {
        public int TalentLinkId { get; set; }

        public int CharacterId { get; set; }

        public int TalentId { get; set; }

    }
}
