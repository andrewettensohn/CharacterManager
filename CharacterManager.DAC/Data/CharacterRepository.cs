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
                .FirstOrDefaultAsync(x => x.CharacterId == id);
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
                .ToListAsync();
        }

        public async Task<Character> NewCharacter(Character character)
        {
            character.Skills = new Skills();
            character.Attributes = new Attributes();
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(NewCharacter), character.CharacterId);

            return character;
        }

        public async Task UpdateCharacter(Character character)
        {

            if (character.XP == 0 && character.Name == "New Character")
            {
                character = ModifyXPForTier(character);
            }

            _context.Update(character);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.CharacterId);
        }

        public async Task UpdateCharacterList(List<Character> updatedCharacters)
        {
            List<Character> allCharacters = await _context.Character.ToListAsync();
            _context.ChangeTracker.Clear();

            List<Character> newCharacters = updatedCharacters.Where(character => !allCharacters.Any(x => x.CharacterId == character.CharacterId)).ToList();

            updatedCharacters.RemoveAll(character => newCharacters.Any(x => x.CharacterId == character.CharacterId));

            _context.UpdateRange(updatedCharacters);
            _context.AddRange(newCharacters);
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
        }

        public async Task<List<Archetype>> GetArchetypes()
        {
            return await _context.Archetype.ToListAsync();
        }

        //private static Character UpdateCharacterXPForNewArchetype(Character character)
        //{

        //    if (character.Archetype.AttributeBonus > 0)
        //    {
        //        character.XP += character.Archetype.AttributeBonus * 4;
        //    }

        //    if (character.Archetype.SkillBonus > 0)
        //    {
        //        character.XP += character.Archetype.SkillBonus * 2;
        //    }

        //    character.XP -= character.Archetype.XPCost;

        //    return character;
        //}

        //private async Task<Character> ResetCharacterXP(ArchetypeLink link, Character character)
        //{
        //    Archetype oldArchetype = await _context.Archetype.FirstOrDefaultAsync(x => x.ArchetypeId == link.ArchetypeId);

        //    if (oldArchetype.AttributeBonus > 0)
        //    {
        //        character.XP -= oldArchetype.AttributeBonus * 4;
        //    }

        //    if (oldArchetype.SkillBonus > 0)
        //    {
        //        character.XP -= oldArchetype.SkillBonus * 2;
        //    }

        //    character.XP += oldArchetype.XPCost;

        //    return character;
        //}


        public async Task AddNewArmor(Armor armor)
        {
            await _context.AddAsync(armor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Armor>> GetArmorList()
        {
            return await _context.Armor.ToListAsync();
        }

        public async Task AddNewGear(Gear gear)
        {
            await _context.AddAsync(gear);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Gear>> GetGearList()
        {
            return await _context.Gear.ToListAsync();
        }

        public async Task AddNewTalent(Talent talent)
        {
            await _context.AddAsync(talent);
            await _context.SaveChangesAsync();
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

        public async Task <List<Transaction>> ListTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddNewWeapon(Weapon weapon)
        {
            await _context.AddAsync(weapon);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Weapon>> GetWeapons()
        {
            return await _context.Weapon.ToListAsync();
        }

        public async Task UpdateSyncTime()
        {

            ConfigParam syncParam = await _context.ConfigParams.FirstOrDefaultAsync(x => x.Name == "LastSyncTime");

            if(syncParam == null)
            {
                syncParam = new ConfigParam
                {
                    Name = "LastSyncTime",
                    Time = DateTime.Now,
                };
            }
            else
            {
                syncParam.Time = DateTime.Now;
            }

            _context.ConfigParams.Update(syncParam);
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime> GetLastSyncTime()
        {
            ConfigParam syncParam = await _context.ConfigParams.FirstOrDefaultAsync(x => x.Name == "LastSyncTime");
            if(syncParam != null)
            {
                return syncParam.Time;
            }
            else
            {
                await UpdateSyncTime();
                return DateTime.MinValue;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
