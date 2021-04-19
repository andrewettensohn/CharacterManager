using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Models.Links;
using CharacterManager.Sync.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharacterManager.Models.Extensions;

namespace CharacterManager.Data
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
            //return await _context.Character
            //    .Include(character => character.Skills)
            //    .Include(character => character.Attributes)
            //    .Include(character => character.Archetype)
            //    .Include(character => character.Armor)
            //    .Include(character => character.CharacterGear)
            //    .Include(character => character.Weapons)
            //    .Include(character => character.Talents)
            //    .FirstOrDefaultAsync(x => x.Id == id);
            CharacterSync syncModel = await _context.CharacterSync.FirstOrDefaultAsync(x => x.Id == id);

            if(syncModel == null)
            {
                return null;
            }

            return syncModel.ConvertSyncModelToCoreModel<Character, CharacterSync>();
        }

        public async Task<List<Character>> ListCharacters()
        {
            //return await _context.Character
            //    .Include(character => character.Skills)
            //    .Include(character => character.Attributes)
            //    .Include(character => character.Archetype)
            //    .Include(character => character.Armor)
            //    .Include(character => character.CharacterGear)
            //    .Include(character => character.Weapons)
            //    .Include(character => character.Talents)
            //    .AsNoTrackingWithIdentityResolution()
            //    .ToListAsync();

            List<CharacterSync> syncModels = await _context.CharacterSync.ToListAsync();
            return syncModels.ConvertSyncModelsToCoreModels<Character, CharacterSync>();
        }

        public async Task<Character> NewCharacter(Character character)
        {
            character.Id = Guid.NewGuid();
            character.Skills = new Skills();
            character.Attributes = new Attributes();
            //await _context.AddAsync(character);
            CharacterSync characterSync = new CharacterSync
            {
                Id = character.Id,
                Json = JsonConvert.SerializeObject(character),
            };
            _context.Add(characterSync);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);

            return character;
        }

        //public async Task SyncNewCharacter(Character character)
        //{
        //    await _context.AddAsync(character);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task SyncUpdateCharacter(Character apiCharacter)
        //{
            
        //    int rows = await _context.SaveChangesAsync();
        //}

        public async Task UpdateCharacter(Character character)
        {

            if (character.XP == 0 && character.Name == "New Character")
            {
                character = ModifyXPForTier(character);
            }

            CharacterSync syncModel = await _context.CharacterSync.FirstOrDefaultAsync(x => x.Id == character.Id);

            syncModel.Json = JsonConvert.SerializeObject(character);

            _context.CharacterSync.Update(syncModel);
            _context.SaveChanges();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);
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
                DateTime = DateTime.UtcNow
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
            SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync(x => x.IsDownSyncStatus == false);

            if(syncStatus == null)
            {
                syncStatus = new SyncStatus();
            }

            syncStatus.GetType().GetProperty(syncName).SetValue(syncStatus, DateTime.UtcNow);

            _context.SyncStatus.Update(syncStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime> GetLastSyncTime(string syncName)
        {
            SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync(x => x.IsDownSyncStatus == false);

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

        public Task SyncNewCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Task SyncUpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }
    }
}
