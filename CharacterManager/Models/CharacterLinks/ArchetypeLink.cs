using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class ArchetypeLink
    {
        public int ArchetypeLinkId { get; set; }
        
        public int ArchetypeId { get; set; }

        public int CharacterId { get; set; }
    }
}
