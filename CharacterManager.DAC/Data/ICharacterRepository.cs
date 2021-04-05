using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data
{
     public interface ICharacterRepository
    {
         Task AddNewArchetype(Archetype archetype);

         Task<Character> UpdateArchetype(Character character);

         Task<Archetype> GetArchetypeForCharacter(Guid characterId);

         Task<List<Archetype>> GetArchetypes();

         Task<Armor> AddArmor(Character character);

         Task<List<Armor>> GetArmorList();

         Task UpdateArmor(Character character);

         Task<Armor> GetArmorForCharacter(Guid characterId);

         Task<List<Character>> ListCharacters();

         Task<Character> NewCharacter(Character character);

         Task<Character> GetCharacter(Guid id);

         Task UpdateCharacter(Character character);

         Task AddNewArmor(Armor armor);

         Task<Attributes> GetCharacterAttributes(Guid id);

         Task<Attributes> AddAttributes(Attributes attributes);

         Task AddNewGear(Gear gear);

         Task<Character> AddGear(Character character, Gear gear);

         Task<Character> AddExistingGearToCharacter(Character character, Gear gear);

         Task<Character> RemoveGearFromCharacter(Character character, Gear gear);

         Task UpdateGearList(List<Gear> gearList);

         Task<List<Gear>> GetGearListForCharacter(Guid characterId);

         Task<List<Gear>> GetGearList();

         Task<Skills> GetCharacterSkills(Guid id);

         Task<Skills> AddSkills(Skills skills);

         Task AddNewTalent(Talent talent);

         Task<Character> AddTalent(Character character, Talent talent);

         Task<Character> AddExistingTalentToCharacter(Character character, Talent talent);

         Task<Character> RemoveTalentFromCharacter(Character character, Talent talent);

         Task RemoveTalent(Talent talent);

         Task<Talent> UpdateTalent(Talent talent);

         Task UpdateTalents(List<Talent> talents);

         Task<List<Talent>> GetTalentsForCharacter(Guid characterId);

         Task<List<Talent>> GetTalents();

        Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId);

        Task AddTransaction(Transaction transaction);

        Task AddTransactionList(List<Transaction> transactions);

        Task UpdateWeapon(Weapon weapon);

        Task AddNewWeapon(Weapon weapon);

        Task<Character> AddWeapon(Character character, Weapon weapon);

        Task<Character> AddExistingWeaponToCharacter(Character character, Weapon weapon);

        Task<Character> RemoveWeaponFromCharacter(Character character, Weapon weapon);

        Task UpdateWeapons(List<Weapon> weapons);

        Task<List<Weapon>> GetWeaponsForCharacter(Guid characterId);

        Task<List<Weapon>> GetWeapons();

    }
}
