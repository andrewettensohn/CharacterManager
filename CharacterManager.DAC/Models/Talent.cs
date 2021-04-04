using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Talent
    {
        public Guid TalentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public int XPCost { get; set; }

    }
}
