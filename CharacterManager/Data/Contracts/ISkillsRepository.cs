using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface ISkillsRepository
    {
        public Task<Skills> GetCharacterSkills(Guid id);

        public Task<Skills> AddSkills(Skills skills);
    }
}
