using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Skills
    {
        public int SkillsId { get; set; }

        public int CharacterId { get; set; }

        public int Athletics { get; set; }

        public int Awareness { get; set; }

        public int Ballistic { get; set; }

        public int Cunning { get; set; }

        public int Deception { get; set; }

        public int Insight { get; set; }

        public int Intimidation { get; set; }

        public int Investigation { get; set; }

        public int Leadership { get; set; }

        public int Medicae { get; set; }

        public int Persuasion { get; set; }

        public int Pilot { get; set; }

        public int Pyschic { get; set; }

        public int Scholar {get; set;}

        public int Stealth { get; set; }

        public int Survival { get; set; }

        public int Tech { get; set; }

        public int Weapon { get; set; }
    }
}
