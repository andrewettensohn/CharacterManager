using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Data;
using CharacterManager.Models;
using CharacterManager.Sync.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Worker
{
    public class DownSyncRestClient : BaseRestClient
    {
        private readonly string _route;
        private readonly string _controller = "downSync";
        private ApplicationDbContext _context;
        private SyncStatus _apiSyncStatus;
        private SyncStatus _localSyncStatus;

        public DownSyncRestClient(ILogger<DownSyncRestClient> logger, HttpClient http, IConfiguration config, IHostEnvironment env, 
            IDbContextFactory<ApplicationDbContext> dbFactory) : base(http)
        {
            _context = dbFactory.CreateDbContext();

            if (env.IsDevelopment())
            {
                //_route = $"{config["Routes:Dev"]}";
                _route = $"{config["Routes:Prod"]}";
                logger.LogInformation($"Using Dev Route, {config["Routes:Dev"]}");
            }
            else
            {
                _route = $"{config["Routes:Prod"]}";
                logger.LogInformation($"Using Prod Route, {config["Routes:Prod"]}");
            }
        }

        public async Task ExecuteDownSync()
        {
            bool isDownSyncApiAvailable = await IsDownSyncApiAvailable();

            if (!isDownSyncApiAvailable) return;

            bool isDatabasePopulated = _context.CharacterSync.Any();

            _apiSyncStatus = await GetApiSyncStatus();
            _localSyncStatus = await GetLocalDownSyncStatus();

            if (!isDatabasePopulated)
            {
                await LoadArchetypes();
                await LoadTalents();
                await LoadArmor();
                await LoadGear();
                await LoadWeapons();
            }
            else
            {

                await SyncCharacters();
                await SyncTalents();
                await SyncArchetypes();
                await SyncArmor();
                await SyncGear();
                await SyncWeapons();
            }

        }

        private async Task<bool> IsDownSyncApiAvailable()
        {
            HttpResponseMessage response = await GetResponseMessage(_route, _controller, "isAvailable");

            return response.IsSuccessStatusCode;
        }

        private async Task<SyncStatus> GetLocalDownSyncStatus()
        {
            SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync(x => x.IsDownSyncStatus == true);

            if (syncStatus == null)
            {
                syncStatus = new SyncStatus { IsDownSyncStatus = true };
                _context.Add(syncStatus);
                _context.SaveChanges();
            }

            return syncStatus;
        }

        private async Task<SyncStatus> GetApiSyncStatus()
        {
            return await GetRequestForItemAsync<SyncStatus>(_route, _controller, "syncStatus");
        }

        private void UpdateLocalDownSyncTime(string syncName)
        {

            _localSyncStatus.GetType().GetProperty(syncName).SetValue(_localSyncStatus, DateTime.UtcNow);

            _context.SyncStatus.Update(_localSyncStatus);
            _context.SaveChanges();
        }

        private async Task SyncTalents()
        {
            if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Talent> apiModels = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

            if (apiModels is null || !apiModels.Any()) return;

            List<Talent> localModels = _context.Talent.AsNoTracking().ToList();

            List<Talent> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Talent> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();

            UpdateLocalDownSyncTime(nameof(SyncStatus.TalentLastSync));
        }

        private async Task SyncArmor()
        {
            if (_localSyncStatus.ArmorLastSync > _apiSyncStatus.ArmorLastSync) return;

            List<Armor> apiModels = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

            if (apiModels is null || !apiModels.Any()) return;

            List<Armor> localModels = _context.Armor.AsNoTracking().ToList();

            List<Armor> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Armor> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncGear()
        {
            if (_localSyncStatus.GearLastSync > _apiSyncStatus.GearLastSync) return;

            List<Gear> apiModels = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

            if (apiModels is null || !apiModels.Any()) return;

            List<Gear> localModels = _context.Gear.AsNoTracking().ToList();

            List<Gear> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Gear> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncWeapons()
        {

            if (_localSyncStatus.WeaponLastSync > _apiSyncStatus.WeaponLastSync) return;

            List<Weapon> apiModels = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

            if (apiModels is null || !apiModels.Any()) return;

            List<Weapon> localModels = _context.Weapon.AsNoTracking().ToList();

            List<Weapon> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Weapon> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncArchetypes()
        {
            if (_localSyncStatus.ArchetypeLastSync > _apiSyncStatus.ArchetypeLastSync) return;

            List<Archetype> apiModels = await GetRequestForListAsync<Archetype>(_route, _controller, "archtypeList");

            if (apiModels is null || !apiModels.Any()) return;

            List<Archetype> localModels = _context.Archetype.AsNoTracking().ToList();

            List<Archetype> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Archetype> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncCharacters()
        {
            if (_localSyncStatus.CharacterLastSync > _apiSyncStatus.CharacterLastSync) return;

            JArray characterArray = await GetListAsJsonAsync(_route, _controller, "characterList");

            if (characterArray is null) return;

            foreach (JObject characterObj in characterArray)
            {

                bool isIdGuid = Guid.TryParse(characterObj["id"].ToString(), out Guid id);
                string characterModelJson = characterObj.ToString();

                if (isIdGuid && !string.IsNullOrWhiteSpace(characterModelJson))
                {
                    CharacterSync characterSync = new CharacterSync
                    {
                        Id = id,
                        Json = characterModelJson
                    };

                    bool isNewCharacter = !_context.CharacterSync.AsNoTracking().Any(x => x.Id == characterSync.Id);

                    if (isNewCharacter)
                    {
                        _context.CharacterSync.Add(characterSync);
                    }
                    else
                    {
                        _context.CharacterSync.Update(characterSync);
                    }
                }
            }

            _context.SaveChanges();
        }

        private List<CoreModel> GetNewApiModelsFromLocalModels<CoreModel>(List<CoreModel> apiModels, List<CoreModel> localModels) where CoreModel : ICoreCharacterModel
        {
            return apiModels.Where(x => !localModels.Any(y => y.Id == x.Id)).ToList();
        }

        private List<CoreModel> RemoveNewModelsFromApiModels<CoreModel>(List<CoreModel> apiModels, List<CoreModel> newModels) where CoreModel : ICoreCharacterModel
        {
            apiModels.RemoveAll(x => newModels.Any(y => y.Id == x.Id));
            return apiModels;
        }


        #region Inital Load

        private async Task LoadArchetypes()
        {
            List<Archetype> models = await GetRequestForListAsync<Archetype>(_route, _controller, "archetypeList");

            if (models is null || !models.Any()) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadTalents()
        {
            List<Talent> models = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

            if (models is null || !models.Any()) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadArmor()
        {
            List<Armor> models = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

            if (models is null || !models.Any()) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadGear()
        {
            List<Gear> models = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

            if (models is null || !models.Any()) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadWeapons()
        {
            List<Weapon> models = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

            if (models is null || !models.Any()) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
