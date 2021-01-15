using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IArmorRepository
    {
        public Task<Armor> AddArmor(Character character);

        public Task<List<Armor>> GetArmorList();

        public Task UpdateArmor(Character character);

        public Task<Armor> GetArmorForCharacter(int characterId);
    }
}
