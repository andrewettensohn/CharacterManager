using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class ArchetypeLink
    {
        public Guid ArchetypeLinkId { get; set; }
        
        public Guid ArchetypeId { get; set; }

        public Guid CharacterId { get; set; }
    }
}
