using CharacterManager.Sync.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class QuestSync : ICharacterManagerSync
    {
        [Key]
        public Guid Id { get; set; }

        public string Json { get; set; }
    }
}
