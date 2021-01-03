using System.Collections.Generic;

namespace CharacterManager.Models
{
    public class Race
    {
        public int RaceId { get; set; }

        public string Name { get; set; }

        public Stat RaceModifer { get; set; } 
    }
}
