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
            CharacterSync syncModel = await _context.CharacterSync.FirstOrDefaultAsync(x => x.Id == id);

            if(syncModel == null)
            {
                return null;
            }

            return syncModel.ConvertSyncModelToCoreModel<Character, CharacterSync>();
        }

        public async Task<List<Character>> ListCharacters()
        {
            List<CharacterSync> syncModels = await _context.CharacterSync.ToListAsync();
            return syncModels.ConvertSyncModelsToCoreModels<Character, CharacterSync>();
        }

        public async Task<Character> NewCharacter(Character character)
        {
            character.Id = Guid.NewGuid();
            character.Skills = new Skills();
            character.Attributes = new Attributes();

            CharacterSync characterSync = new CharacterSync
            {
                Id = character.Id,
                Json = JsonConvert.SerializeObject(character),
            };
            _context.CharacterSync.Add(characterSync);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);

            return character;
        }

        public async Task UpdateCharacter(Character character)
        {

            CharacterSync syncModel = await _context.CharacterSync.FirstOrDefaultAsync(x => x.Id == character.Id);

            syncModel.Json = JsonConvert.SerializeObject(character);

            _context.CharacterSync.Update(syncModel);
            _context.SaveChanges();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateCharacter), character.Id);
        }

        public async Task NewQuest(Quest quest)
        {
            quest.Id = new Guid();

            QuestSync questSync = new QuestSync
            {
                Id = quest.Id,
                Json = JsonConvert.SerializeObject(quest)
            };

            _context.QuestSync.Add(questSync);
            _context.SaveChanges();

            await AddNewTransaction(nameof(CharacterRepository), nameof(NewQuest), quest.Id);
        }

        public async Task UpdateQuest(Quest quest)
        {
            QuestSync questSync = await _context.QuestSync.FirstOrDefaultAsync(x => x.Id == quest.Id);

            questSync.Json = JsonConvert.SerializeObject(quest);

            _context.QuestSync.Update(questSync);
            _context.SaveChanges();

            await AddNewTransaction(nameof(CharacterRepository), nameof(UpdateQuest), quest.Id);
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
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAfterLastSyncTimeForSourceMethod(DateTime lastSyncTime, string sourceMethod)
        {
            return await _context.Transactions.Where(x => x.DateTime > lastSyncTime && x.SourceMethod == sourceMethod).ToListAsync();
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

        public async Task AddNewPyschicPower(PyschicPower pyschicPower)
        {
            await _context.AddAsync(pyschicPower);
            await _context.SaveChangesAsync();

            await AddNewTransaction(nameof(CharacterRepository), nameof(AddNewPyschicPower), pyschicPower.Id);
        }

        public async Task<List<PyschicPower>> GetPyschicPowers()
        {
            return await _context.PsychicPowers.ToListAsync();
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

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
