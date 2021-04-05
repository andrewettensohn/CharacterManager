using CharacterManager.Models.CharacterLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterManager.Models
{
    public class Character
    {
        public Guid CharacterId { get; set; }

        public string Name { get; set; }

        public int XP { get; set; }

        public int Tier { get; set; }

        public int Rank { get; set; }

        public int Wrath { get; set; }

        public int Glory { get; set; }

        public Attributes Attributes { get; set; }

        public Skills Skills { get; set; }

        public Guid ArchetypeId { get; set; }
        public Archetype Archetype { get; set; }

        [NotMapped]
        public Armor Armor { get; set; }

        [NotMapped]
        public List<Talent> Talents { get; set; }

        [NotMapped]
        public List<Weapon> Weapons { get; set; }

        [NotMapped]
        public List<Gear> Gear { get; set; }

        public ArchetypeLink ArchetypeLink { get; set; }

        public ArmorLink ArmorLink { get; set; }

        public List<TalentLink> GearLinks { get; set; }

        public List<WeaponLink> WeaponLinks { get; set; }

    }
}
