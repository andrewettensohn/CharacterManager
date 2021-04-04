using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Contracts
{
    public interface IAttributeRepository
    {
        public Task<Attributes> GetCharacterAttributes(Guid id);

        public Task<Attributes> AddAttributes(Attributes attributes);
    }
}
