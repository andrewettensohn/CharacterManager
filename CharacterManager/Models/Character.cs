using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterManager.Models
{
    public class Character
    {
        public int CharacterId { get; set; }

        public string Name { get; set; }

        public int XP { get; set; }

        public int Tier { get; set; }

        public int Rank { get; set; }

        public Attributes Attributes { get; set; }

        public Skills Skills { get; set; }

        [NotMapped]
        public Armor Armor { get; set; }

        [NotMapped]
        public Archetype Archetype { get; set; }

        [NotMapped]
        public List<Talent> Talents { get; set; }

        [NotMapped]
        public List<Weapon> Weapons { get; set; }

        [NotMapped]
        public List<Gear> Gear { get; set; }

    }
}
