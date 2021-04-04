using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Contracts
{
    public interface ITalentRepository
    {
        Task AddNewTalent(Talent talent);
        public Task<Character> AddTalent(Character character, Talent talent);

        public Task<Character> AddExistingTalentToCharacter(Character character, Talent talent);

        public Task<Character> RemoveTalentFromCharacter(Character character, Talent talent);

        public Task RemoveTalent(Talent talent);

        public Task<Talent> UpdateTalent(Talent talent);

        public Task UpdateTalents(List<Talent> talents);

        public Task<List<Talent>> GetTalentsForCharacter(Guid characterId);

        public Task<List<Talent>> GetTalents();
    }
}
