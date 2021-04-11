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

        Task UpdateArchetypeList(List<Archetype> updatedArchetypes);

         Task<List<Archetype>> GetArchetypes();

        Task AddNewArmor(Armor armor);

        Task<List<Armor>> GetArmorList();

        Task UpdateArmorList(List<Armor> updatedArmor);

         Task<List<Character>> ListCharacters();

         Task<Character> NewCharacter(Character character);

        Task SyncNewCharacter(Character character);

        Task SyncUpdateCharacter(Character character);

         Task<Character> GetCharacter(Guid id);

         Task UpdateCharacter(Character character);

         Task AddNewGear(Gear gear);

         Task<List<Gear>> GetGearList();

         Task AddNewTalent(Talent talent);

         Task<List<Talent>> GetTalents();

        Task UpdateGearList(List<Gear> updatedGear);

        Task UpdateWeaponList(List<Weapon> updatedWeapons);

        Task UpdateTalentList(List<Talent> updatedTalents);

        Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId);

        Task AddTransaction(Transaction transaction);

        Task AddTransactionList(List<Transaction> transactions);

        Task<List<Transaction>> GetTransactionsAfterLastSyncTime(DateTime lastSyncTime);

        Task<List<Transaction>> GetTransactionsAfterLastSyncTimeForSourceMethod(DateTime lastSyncTime, string sourceMethod);

        Task<List<Transaction>> ListTransactions();

        Task AddNewWeapon(Weapon weapon);

        Task<List<Weapon>> GetWeapons();

        Task UpdateSyncTime(string syncName);

        Task<DateTime> GetLastSyncTime(string syncName);

    }
}
