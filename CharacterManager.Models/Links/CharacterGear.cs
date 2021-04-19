using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models.Links
{
    public class CharacterGear
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; }
        public Guid GearId { get; set; }
        public Gear Gear { get; set; }
    }
}
