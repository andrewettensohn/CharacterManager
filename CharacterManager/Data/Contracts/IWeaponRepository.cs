using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IWeaponRepository
    {
        public Task<Character> AddWeapon(Character character, Weapon weapon);

        public Task<Character> AddExistingWeaponToCharacter(Character character, Weapon weapon);

        public Task<Character> RemoveWeaponFromCharacter(Character character, Weapon weapon);

        public Task UpdateWeapons(List<Weapon> weapons);

        public Task<List<Weapon>> GetWeaponsForCharacter(int characterId);

        public Task<List<Weapon>> GetWeapons();

    }
}
