using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Contracts
{
    public interface IArmorRepository
    {
        Task AddNewArmor(Armor armor);

        public Task<Armor> AddArmor(Character character);

        public Task<List<Armor>> GetArmorList();

        public Task UpdateArmor(Character character);

        public Task<Armor> GetArmorForCharacter(Guid characterId);
    }
}
