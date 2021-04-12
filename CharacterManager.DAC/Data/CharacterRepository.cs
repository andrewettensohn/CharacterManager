using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Sync.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data
{
    public class CharacterRepository : ICharacterRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task<Character> GetCharacter(Guid id)
        {
            return await _context.Character
                .Include(character => character.Skills)
                .Include(character => character.Attributes)
                .Include(character => character.Archetype)
                .Include(character => character.Armor)
                .Include(character => character.CharacterGear)
                .Include(character => character.Weapons)
                .Include(character => character.Talents)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Character>> ListCharacters()
        {
            return await _context.Character
                .Include(character => character.Skills)
                .Include(character => character.Attributes)
                .Include(character => character.Archetype)
                .Include(character => character.Armor)
                .Include(character => character.CharacterGear)
                .Include(character => character.Weapons)
                .Include(character => character.Talents)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<Character> NewCharacter(Character character)
        {
            character.Skills = new Skills();
            character.Attributes = new Attributes();
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);

            return character;
        }

        public async Task SyncNewCharacter(Character character)
        {
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();
        }

        public async Task SyncUpdateCharacter(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCharacter(Character character)
        {

            if (character.XP == 0 && character.Name == "New Character")
            {
                character = ModifyXPForTier(character);
            }

            _context.Update(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);
        }

        public async Task UpdateArmorList(List<Armor> updatedArmor)
        {
            List<Armor> allArmor = await _context.Armor.ToListAsync();

            List<Armor> newArmor = updatedArmor.Where(character => !allArmor.Any(x => x.Id == character.Id)).ToList();

            updatedArmor.RemoveAll(character => newArmor.Any(x => x.Id == character.Id));
            newArmor.RemoveAll(x => !allArmor.Any(y => y.Name == x.Name));

            _context.UpdateRange(updatedArmor);
            _context.AddRange(newArmor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArchetypeList(List<Archetype> updatedArchetypes)
        {
            List<Archetype> allArchetypes = await _context.Archetype.ToListAsync();

            List<Archetype> newArchetypes = updatedArchetypes.Where(character => !allArchetypes.Any(x => x.Id == character.Id)).ToList();

            updatedArchetypes.RemoveAll(character => newArchetypes.Any(x => x.Id == character.Id));
            newArchetypes.RemoveAll(x => !allArchetypes.Any(y => y.Name == x.Name));

            _context.UpdateRange(updatedArchetypes);
            _context.AddRange(newArchetypes);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTalentList(List<Talent> updatedTalents)
        {
            List<Talent> allTalents = await _context.Talent.ToListAsync();

            List<Talent> newTalents = updatedTalents.Where(character => !allTalents.Any(x => x.Id == character.Id)).ToList();

            updatedTalents.RemoveAll(character => newTalents.Any(x => x.Id == character.Id));
            newTalents.RemoveAll(x => !allTalents.Any(y => y.Name == x.Name));

            _context.UpdateRange(updatedTalents);
            _context.AddRange(newTalents);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGearList(List<Gear> updatedGear)
        {
            List<Gear> allGear = await _context.Gear.ToListAsync();

            List<Gear> newGear = updatedGear.Where(character => !allGear.Any(x => x.Id == character.Id)).ToList();

            updatedGear.RemoveAll(character => newGear.Any(x => x.Id == character.Id));
            newGear.RemoveAll(x => !allGear.Any(y => y.Name == x.Name));

            _context.UpdateRange(updatedGear);
            _context.AddRange(newGear);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWeaponList(List<Weapon> updatedWeapons)
        {
            List<Weapon> allWeapons = await _context.Weapon.ToListAsync();

            List<Weapon> newWeapons = updatedWeapons.Where(character => !allWeapons.Any(x => x.Id == character.Id)).ToList();

            updatedWeapons.RemoveAll(character => newWeapons.Any(x => x.Id == character.Id));
            newWeapons.RemoveAll(x => !allWeapons.Any(y => y.Name == x.Name));

            _context.UpdateRange(updatedWeapons);
            _context.AddRange(newWeapons);
            await _context.SaveChangesAsync();
        }

        private static Character ModifyXPForTier(Character character)
        {
            if (character.Tier == 1)
            {
                character.XP = 100;
            }
            else if (character.Tier == 2)
            {
                character.XP = 200;
            }
            else if (character.Tier == 3)
            {
                character.XP = 300;
            }

            return character;
        }

        public async Task AddNewArchetype(Archetype archetype)
        {
            await _context.AddAsync(archetype);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewArchetype), archetype.Id);
        }

        public async Task<List<Archetype>> GetArchetypes()
        {
            return await _context.Archetype.ToListAsync();
        }


        public async Task AddNewArmor(Armor armor)
        {
            await _context.AddAsync(armor);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewArmor), armor.Id);
        }

        public async Task<List<Armor>> GetArmorList()
        {
            return await _context.Armor.ToListAsync();
        }

        public async Task AddNewGear(Gear gear)
        {
            await _context.AddAsync(gear);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewGear), gear.Id);
        }

        public async Task<List<Gear>> GetGearList()
        {
            return await _context.Gear.ToListAsync();
        }

        public async Task AddNewTalent(Talent talent)
        {
            await _context.AddAsync(talent);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewTalent), talent.Id);
        }

        public async Task<List<Talent>> GetTalents()
        {
            return await _context.Talent.ToListAsync();
        }


        public async Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId)
        {
            Transaction transaction = new Transaction
            {
                SourceRepository = sourceRepo,
                SourceMethod = methodName,
                SourceId = SourceId,
                DateTime = DateTime.Now
            };
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransactionList(List<Transaction> transactions)
        {
            List<Transaction> alltransactions = await _context.Transactions.ToListAsync();
            _context.ChangeTracker.Clear();

            transactions.RemoveAll(x => alltransactions.Any(transaction => transaction.TransactionId == x.TransactionId));

            await _context.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAfterLastSyncTime(DateTime lastSyncTime)
        {
            return await _context.Transactions.Where(x => x.DateTime > lastSyncTime).ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAfterLastSyncTimeForSourceMethod(DateTime lastSyncTime, string sourceMethod)
        {
            return await _context.Transactions.Where(x => x.DateTime > lastSyncTime && x.SourceMethod == sourceMethod).ToListAsync();
        }

        public async Task <List<Transaction>> ListTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddNewWeapon(Weapon weapon)
        {
            await _context.AddAsync(weapon);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewWeapon), weapon.Id);
        }

        public async Task<List<Weapon>> GetWeapons()
        {
            return await _context.Weapon.ToListAsync();
        }

        public async Task UpdateSyncTime(string syncName)
        {
            SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync();

            if(syncStatus == null)
            {
                syncStatus = new SyncStatus();
            }

            syncStatus.GetType().GetProperty(syncName).SetValue(syncStatus, DateTime.Now);

            _context.SyncStatus.Update(syncStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime> GetLastSyncTime(string syncName)
        {
            SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync();

            if (syncStatus == null)
            {
                syncStatus = new SyncStatus();

                _context.SyncStatus.Update(syncStatus);
                await _context.SaveChangesAsync();
            }

            return (DateTime)syncStatus.GetType().GetProperty(syncName).GetValue(syncStatus);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
