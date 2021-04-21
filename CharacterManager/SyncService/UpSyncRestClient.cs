using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Data;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CharacterManager.Worker
{
    public class UpSyncRestClient : BaseRestClient
    {
        private readonly IConfiguration _config;
        private ICharacterRepository _characterRepository;
        private readonly string _route;
        private readonly string _controller = "upSync";
        private readonly SyncStatus _localSyncStatus;
        private readonly ApplicationDbContext _context;

        public UpSyncRestClient(HttpClient http, IConfiguration config, IHostEnvironment env, IDbContextFactory<ApplicationDbContext> dbFactory,
            ICharacterRepository characterRepository) : base(http)
        {
            _config = config ?? throw new ArgumentNullException();
            _context = dbFactory.CreateDbContext();
            _localSyncStatus = GetUpSyncStatus();
            _characterRepository = characterRepository;

            if (env.IsDevelopment())
            {
                //_route = $"{config["Routes:Dev"]}";
                _route = $"{config["Routes:Prod"]}";
            }
            else
            {
                _route = $"{config["Routes:Prod"]}";
            }
        }

        public async Task ExecuteUpSync()
        {
            bool isUpSyncApiAvailable = await IsUpSyncApiAvailable();

            if (!isUpSyncApiAvailable) return;

            await SyncCharacters();
            await SyncArchetypes();
            await SyncArmor();
            await SyncGear();
            await SyncTalent();
            await SyncWeapons();
            
        }

        private SyncStatus GetUpSyncStatus()
        {
            SyncStatus syncStatus = _context.SyncStatus.FirstOrDefault(x => x.IsDownSyncStatus == false);

            if (syncStatus == null)
            {
                syncStatus = new SyncStatus { IsDownSyncStatus = false };
                _context.Add(syncStatus);
                _context.SaveChanges();
            }

            return syncStatus;
        }

        private async Task<bool> IsUpSyncApiAvailable()
        {
            HttpResponseMessage response = await GetResponseMessage(_route, _controller, "isAvailable");

            return response.IsSuccessStatusCode;
        }

        private async Task SyncCharacters()
        {
            List<Transaction> newTransactions = await _characterRepository
                .GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.CharacterLastSync, nameof(CharacterRepository.UpdateCharacter));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Character> allCharacters = await _characterRepository.ListCharacters();

            HttpResponseMessage response =  await PostContent(allCharacters, _route, _controller, "characterList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.CharacterLastSync));
            }
        }

        private async Task SyncArchetypes()
        {
            List<Transaction> newTransactions = await _characterRepository.
                GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.ArchetypeLastSync, nameof(CharacterRepository.AddNewArchetype));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Archetype> allArchetype = await _characterRepository.GetArchetypes();

            HttpResponseMessage response =  await PostContent(allArchetype, _route, _controller, "archetypeList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.ArchetypeLastSync));
            }
        }

        private async Task SyncArmor()
        {
            List<Transaction> newTransactions = await _characterRepository.
                GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.ArmorLastSync, nameof(CharacterRepository.AddNewArmor));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Armor> allArmor = await _characterRepository.GetArmorList();

            HttpResponseMessage response =  await PostContent(allArmor, _route, _controller, "armorList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.ArmorLastSync));
            }
        }

        private async Task SyncGear()
        {
            List<Transaction> newTransactions = await _characterRepository.
                GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.GearLastSync, nameof(CharacterRepository.AddNewGear));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Gear> allGear = await _characterRepository.GetGearList();

            HttpResponseMessage response =  await PostContent(allGear, _route, _controller, "gearList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.GearLastSync));
            }
        }

        private async Task SyncTalent()
        {
            List<Transaction> newTransactions = await _characterRepository.
                GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.TalentLastSync, nameof(CharacterRepository.AddNewTalent));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Talent> allTalents = await _characterRepository.GetTalents();

            HttpResponseMessage response = await PostContent(allTalents, _route, _controller, "talentList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.TalentLastSync));
            }
        }

        private async Task SyncWeapons()
        {
            List<Transaction> newTransactions = await _characterRepository.
                GetTransactionsAfterLastSyncTimeForSourceMethod(_localSyncStatus.WeaponLastSync, nameof(CharacterRepository.AddNewWeapon));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Weapon> allWeapons = await _characterRepository.GetWeapons();

            HttpResponseMessage response = await PostContent(allWeapons, _route, _controller, "weaponList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.WeaponLastSync));
            }
        }

    }
}
