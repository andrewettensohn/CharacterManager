using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Sync.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly string _route;
        private readonly string _controller = "downSync";
        private ApplicationDbContext _context;
        private SyncStatus _apiSyncStatus;
        private SyncStatus _localSyncStatus;

        public DownSyncRestClient(ILogger<DownSyncRestClient> logger, HttpClient http, IConfiguration config, IHostEnvironment env, IServiceScopeFactory serviceScopeFactory) 
            : base(http)
        {
            _config = config ?? throw new ArgumentNullException();
            _scopeFactory = serviceScopeFactory;

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

        public async Task ExecuteUpSync()
        {
            bool isDownSyncApiAvailable = await IsDownSyncApiAvailable();

            if (!isDownSyncApiAvailable) return;

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IDbContextFactory<ApplicationDbContext> dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();

                _context = dbFactory.CreateDbContext();

                bool isDatabasePopulated = _context.Character.Any() && _context.Talent.Any() && _context.Archetype.Any();

                //TODO: Find a better check to see if the whole database is empty
                if (!isDatabasePopulated)
                {
                    await LoadCharacters();
                    await LoadArchetypes();
                    await LoadTalents();
                    await LoadArmor();
                    await LoadGear();
                    await LoadWeapons();
                }
                else
                {
                    _apiSyncStatus = await GetApiSyncStatus();
                    _localSyncStatus = await GetLocalDownSyncStatus();

                    await SyncTalents();
                    await SyncArchetypes();
                    await SyncCharacters();
                    await SyncArmor();
                    await SyncGear();
                    await SyncWeapons();
                }
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
            //TODO: Might need another local sync status object for upsync vs down sync status
            //has a talent been added on the API since the last down sync
            if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Talent> apiModels = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

            if (!apiModels.Any()) return;

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
            //TODO: Might need another local sync status object for upsync vs down sync status
            //if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Armor> apiModels = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

            if (!apiModels.Any()) return;

            List<Armor> localModels = _context.Armor.AsNoTracking().ToList();

            List<Armor> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Armor> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncGear()
        {
            //TODO: Might need another local sync status object for upsync vs down sync status
            //if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Gear> apiModels = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

            if (!apiModels.Any()) return;

            List<Gear> localModels = _context.Gear.AsNoTracking().ToList();

            List<Gear> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Gear> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncWeapons()
        {
            //TODO: Might need another local sync status object for upsync vs down sync status
            //if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Weapon> apiModels = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

            if (!apiModels.Any()) return;

            List<Weapon> localModels = _context.Weapon.AsNoTracking().ToList();

            List<Weapon> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Weapon> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncArchetypes()
        {
            //TODO: Might need another local sync status object for upsync vs down sync status
            //if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Archetype> apiModels = await GetRequestForListAsync<Archetype>(_route, _controller, "archtypeList");

            if (!apiModels.Any()) return;

            List<Archetype> localModels = _context.Archetype.AsNoTracking().ToList();

            List<Archetype> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Archetype> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
            _context.SaveChanges();
        }

        private async Task SyncCharacters()
        {
            //TODO: Might need another local sync status object for upsync vs down sync status
            //if (_localSyncStatus.TalentLastSync > _apiSyncStatus.TalentLastSync) return;

            List<Character> apiModels = await GetRequestForListAsync<Character>(_route, _controller, "characterList");

            if (!apiModels.Any()) return;

            List<Character> localModels = _context.Character.AsNoTracking().ToList();

            List<Character> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
            List<Character> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);
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

        private async Task LoadCharacters()
        {
            List<Character> models = await GetRequestForListAsync<Character>(_route, _controller, "characterList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadArchetypes()
        {
            List<Archetype> models = await GetRequestForListAsync<Archetype>(_route, _controller, "archetypeList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadTalents()
        {
            List<Talent> models = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadArmor()
        {
            List<Armor> models = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadGear()
        {
            List<Gear> models = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }

        private async Task LoadWeapons()
        {
            List<Weapon> models = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

            if (models is null) return;

            _context.AddRange(models);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
