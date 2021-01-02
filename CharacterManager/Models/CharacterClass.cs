using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class CharacterClass
    {
        public int CharacterClassId { get; set; }

        public string Name { get; set; }

        public ICollection<Stat> ClassStatModifiers { get; set; }

        public ICollection<Spell> ClassSpells { get; set; }

        public ICollection<CharacterAction> ClassActions { get; set; }
    }
}
