using CharacterManager.DAC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Talent : ICoreCharacterModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public int XPCost { get; set; }

        [JsonIgnore]
        public List<Character> Characters { get; set; }

    }
}
