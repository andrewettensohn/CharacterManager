using System.Collections.Generic;

namespace CharacterManager.Models
{
    public class Character
    {
        public int CharacterId { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public ICollection<CharacterAction> Actions { get; set; }

        public ICollection<Equipment> Equipment { get; set; }

        public ICollection<Spell> Spells { get; set; }

        public ICollection<Stat> Stats { get; set; }

        public ICollection<Feature> Features { get; set; }

        public Race Race { get; set; }

        public CharacterClass Class { get; set; }

        public int Currency { get; set; }
    }
}
