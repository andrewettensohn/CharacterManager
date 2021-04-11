using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Models;
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
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private ICharacterRepository _characterRepository;
        private readonly string _route;
        private readonly string _controller = "upSync";

        public UpSyncRestClient(HttpClient http, IConfiguration config, IHostEnvironment env, IServiceScopeFactory serviceScopeFactory) : base(http)
        {
            _config = config ?? throw new ArgumentNullException();
            _scopeFactory = serviceScopeFactory;

            if (env.IsDevelopment())
            {
                _route = $"{config["Routes:Dev"]}";
            }
            else
            {
                //TODO: Add Production route
            }
        }

        public async Task ExecuteUpSync()
        {
            bool isUpSyncApiAvailable = await IsUpSyncApiAvailable();

            if (!isUpSyncApiAvailable) return;

            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                _characterRepository = scope.ServiceProvider.GetRequiredService<ICharacterRepository>();

                //await SyncTransactions();
                await SyncCharacters();
                await SyncArchetypes();
                await SyncArmor();
                await SyncGear();
                await SyncTalent();
                await SyncWeapons();
            }
        }

        private async Task<bool> IsUpSyncApiAvailable()
        {
            HttpResponseMessage response = await GetContent(_route, "upSync", "isAvailable");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task SyncTransactions()
        //{
        //    DateTime lastTransactionSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncTransactions));
        //    List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTime(lastTransactionSyncTime);

        //    bool isSyncNeeded = newTransactions.Any();

        //    if (!isSyncNeeded) return;

        //    HttpResponseMessage response = await PostContent(newTransactions, _route, "upSync", "transactionList");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        await _characterRepository.UpdateSyncTime(nameof(SyncTransactions));
        //    }
        //}

        private async Task SyncCharacters()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.CharacterLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.UpdateCharacter));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List <Guid> characterTransactionIds = newTransactions
                        .Where(x => x.SourceMethod == nameof(CharacterRepository.UpdateCharacter))
                        .Select(x => x.SourceId).ToList();

            List<Character> allCharacters = await _characterRepository.ListCharacters();

            List<Character> updatedCharacters = allCharacters.Where(x => characterTransactionIds.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response =  await PostContent(updatedCharacters, _route, _controller, "characterList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.CharacterLastSync));
            }
        }

        private async Task SyncArchetypes()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.ArchetypeLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.AddNewArchetype));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Guid> archetypeTransactionIds = newTransactions
                .Where(x => x.SourceMethod == nameof(CharacterRepository.AddNewArchetype))
                .Select(x => x.SourceId).ToList();

            List<Archetype> allArchetype = await _characterRepository.GetArchetypes();

            List<Archetype> updatedArchetypes = allArchetype.Where(x => archetypeTransactionIds.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response =  await PostContent(updatedArchetypes, _route, _controller, "archetypeList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.ArchetypeLastSync));
            }
        }

        private async Task SyncArmor()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.ArmorLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.AddNewArmor));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Guid> ids = newTransactions
                .Where(x => x.SourceMethod == nameof(CharacterRepository.AddNewArmor))
                .Select(x => x.SourceId).ToList();

            List<Armor> allArmor = await _characterRepository.GetArmorList();

            List<Armor> updatedArmor = allArmor.Where(x => ids.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response =  await PostContent(updatedArmor, _route, _controller, "armorList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.ArmorLastSync));
            }
        }

        private async Task SyncGear()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.GearLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.AddNewGear));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Guid> ids = newTransactions
                .Where(x => x.SourceMethod == nameof(CharacterRepository.AddNewGear))
                .Select(x => x.SourceId).ToList();

            List<Gear> allGear = await _characterRepository.GetGearList();

            List<Gear> updatedGear = allGear.Where(x => ids.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response =  await PostContent(updatedGear, _route, _controller, "gearList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.GearLastSync));
            }
        }

        private async Task SyncTalent()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.TalentLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.AddNewTalent));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Guid> ids = newTransactions
                .Where(x => x.SourceMethod == nameof(CharacterRepository.AddNewTalent))
                .Select(x => x.SourceId).ToList();

            List<Talent> allTalents = await _characterRepository.GetTalents();

            List<Talent> updatedTalents = allTalents.Where(x => ids.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response = await PostContent(updatedTalents, _route, _controller, "talentList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.TalentLastSync));
            }
        }

        private async Task SyncWeapons()
        {
            DateTime lastSyncTime = await _characterRepository.GetLastSyncTime(nameof(SyncStatus.WeaponLastSync));
            List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTimeForSourceMethod(lastSyncTime, nameof(CharacterRepository.AddNewWeapon));

            bool isSyncNeeded = newTransactions.Any();

            if (!isSyncNeeded) return;

            List<Guid> ids = newTransactions
                .Where(x => x.SourceMethod == nameof(CharacterRepository.AddNewWeapon))
                .Select(x => x.SourceId).ToList();

            List<Weapon> allWeapons = await _characterRepository.GetWeapons();

            List<Weapon> updatedWeapons = allWeapons.Where(x => ids.Any(id => id == x.Id)).ToList();

            HttpResponseMessage response = await PostContent(updatedWeapons, _route, _controller, "weaponList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime(nameof(SyncStatus.WeaponLastSync));
            }
        }

    }
}
