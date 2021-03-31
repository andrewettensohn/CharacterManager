using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IGearRepository
    {
        Task AddNewGear(Gear gear);

        public Task<Character> AddGear(Character character, Gear gear);

        public Task<Character> AddExistingGearToCharacter(Character character, Gear gear);

        public Task<Character> RemoveGearFromCharacter(Character character, Gear gear);

        public Task UpdateGearList(List<Gear> gearList);

        public Task<List<Gear>> GetGearListForCharacter(int characterId);

        public Task<List<Gear>> GetGearList();
    }
}
