using CharacterManager.DAC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterManager.Models
{
    public class Character : ICoreCharacterModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int XP { get; set; }

        public int CurrentWounds { get; set; }

        public int CurrentShock { get; set; }

        public int Tier { get; set; }

        public int Rank { get; set; }

        public int Wrath { get; set; }

        public int Glory { get; set; }

        public string AvatarPath { get; set; }

        public Attributes Attributes { get; set; }

        public Skills Skills { get; set; }

        public Archetype Archetype { get; set; }

        public Armor Armor { get; set; }

        public List<Talent> Talents { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<Gear> CharacterGear { get; set; }

        public List<PyschicPower> PsychicPowers { get; set; }

    }
}
