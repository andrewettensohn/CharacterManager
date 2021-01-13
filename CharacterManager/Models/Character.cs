using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterManager.Models
{
    public class Character
    {
        public int CharacterId { get; set; }

        public string Name { get; set; }

        public int XP { get; set; }

        public Tier Tier { get; set; }

        public Archetype Archetype { get; set; }

        public Attributes Attributes { get; set; }

        public Skills Skills { get; set; }

        public MentalTraits MentalTraits { get; set; }

        public SocialTraits SocialTraits { get; set; }

        public CombatTraits CombatTraits { get; set; }

        [NotMapped]
        public List<Talent> Talents { get; set; }

        [NotMapped]
        public List<Wargear> Wargear { get; set; }

    }
}
