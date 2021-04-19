using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Models
{
    public interface ICharacterManagerSync
    {
        public Guid Id { get; set; }
        public string Json { get; set; }

    }
}
