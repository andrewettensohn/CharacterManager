using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class TalentLink
    {
        public Guid TalentLinkId { get; set; }

        public Guid CharacterId { get; set; }

        public Guid TalentId { get; set; }

    }
}
