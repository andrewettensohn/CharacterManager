using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterManager.Models
{
    public class Archetype
    {
        public int ArchetypeId { get; set; }

        public string Name { get; set; }

        public ArchetypeAbility Ability { get; set; }

        public string Keywords { get; set; }
    }
}
