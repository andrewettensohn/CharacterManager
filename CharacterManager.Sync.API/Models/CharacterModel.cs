using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Models
{
    public class CharacterModel
    {
        [Key]
        public Guid CharacterId { get; set; }

        public string CharacterJson { get; set; }
    }
}
