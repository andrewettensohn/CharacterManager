using CharacterManager.DAC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Quest : ICoreCharacterModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsMainQuest { get; set; }

        public bool IsComplete { get; set; }

        public string BriefDescription { get; set; }

        public string FullDescription { get; set; }
    }
}
