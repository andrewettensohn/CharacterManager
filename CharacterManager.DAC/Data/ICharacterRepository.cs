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

         Task<List<Archetype>> GetArchetypes();

        Task AddNewArmor(Armor armor);

        Task<List<Armor>> GetArmorList();

         Task<List<Character>> ListCharacters();

         Task<Character> NewCharacter(Character character);

         Task<Character> GetCharacter(Guid id);

         Task UpdateCharacter(Character character);

         Task UpdateCharacterList(List<Character> characters);

         Task AddNewGear(Gear gear);

         Task<List<Gear>> GetGearList();

         Task AddNewTalent(Talent talent);

         Task<List<Talent>> GetTalents();

        Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId);

        Task AddTransaction(Transaction transaction);

        Task AddTransactionList(List<Transaction> transactions);

        Task<List<Transaction>> GetTransactionsAfterLastSyncTime(DateTime lastSyncTime);

        Task<List<Transaction>> ListTransactions();

        Task AddNewWeapon(Weapon weapon);

        Task<List<Weapon>> GetWeapons();

        Task UpdateSyncTime();

        Task<DateTime> GetLastSyncTime();

    }
}
