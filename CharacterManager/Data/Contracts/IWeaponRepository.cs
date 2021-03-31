using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data.Contracts
{
    public interface IWeaponRepository
    {

        Task UpdateWeapon(Weapon weapon);

        Task AddNewWeapon(Weapon weapon);

        Task<Character> AddWeapon(Character character, Weapon weapon);

        Task<Character> AddExistingWeaponToCharacter(Character character, Weapon weapon);

        Task<Character> RemoveWeaponFromCharacter(Character character, Weapon weapon);

        Task UpdateWeapons(List<Weapon> weapons);

        Task<List<Weapon>> GetWeaponsForCharacter(int characterId);

        Task<List<Weapon>> GetWeapons();

    }
}
