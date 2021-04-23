using CharacterManager.DAC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class PyschicPower : ICoreCharacterModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Discipline { get; set; }

        public int DN { get; set; }

        public string Activation { get; set; }

        public string Duration { get; set; }

        public string Range { get; set; }

        public string Effect { get; set; }

        public bool MultiTarget { get; set; }

        public string Keywords { get; set; }

        public string Potency { get; set; }

        public string Requirements { get; set; }

        public int XPCost { get; set; }
    }
}
