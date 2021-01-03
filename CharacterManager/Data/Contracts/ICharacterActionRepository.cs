using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface ICharacterActionRepository
    {

        public Task<List<CharacterAction>> ListCharacterActions();

        public Task AddCharacterAction(CharacterAction characterAction);

        public Task UpdateCharacterAction(CharacterAction characterAction);

        public Task DeleteCharacterAction(CharacterAction characterAction);

    }
}
