using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Models
{
    public class WeaponSync : ICharacterManagerSync
    {
        [Key]
        public Guid Id { get; set; }

        public string Json { get; set; }
    }
}
