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
    public class DownSyncRestClient : BaseRestClient, IDisposable
    {
        private readonly string _route;
        private readonly string _controller = "downSync";
        private ApplicationDbContext _context;
        private SyncStatus _apiSyncStatus;
        private ILogger<DownSyncRestClient> _logger;

        public DownSyncRestClient(ILogger<DownSyncRestClient> logger, HttpClient http, IConfiguration config, IHostEnvironment env, 
            IDbContextFactory<ApplicationDbContext> dbFactory) : base(http)
        {
            _context = dbFactory.CreateDbContext();
            _logger = logger;

            if (env.IsDevelopment())
            {
                _route = $"{config["Routes:Dev"]}";
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

            if (!isDatabasePopulated)
            {
                await LoadCharacters();
                await LoadArchetypes();
                await LoadTalents();
                await LoadArmor();
                await LoadGear();
                await LoadWeapons();
                await LoadPyschic();
            }
            else
            {
                await SyncCharacters();
                await SyncTalents();
                await SyncArchetypes();
                await SyncArmor();
                await SyncGear();
                await SyncWeapons();
                await SyncPyschic();
            }

        }

        private async Task<bool> IsDownSyncApiAvailable()
        {
            HttpResponseMessage response = await GetResponseMessage(_route, _controller, "isAvailable");

            return response.IsSuccessStatusCode;
        }

        private async Task<SyncStatus> GetApiSyncStatus()
        {
            SyncStatus apiStatus = await GetRequestForItemAsync<SyncStatus>(_route, _controller, "syncStatus");

            if(apiStatus != null)
            {
                return apiStatus;
            }
            else
            {
                return new SyncStatus();
            }
        }

        private async Task SyncTalents()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewTalent)).DateTime;

                if (_apiSyncStatus.TalentLastSync < lastLocalTransaction) return;

                List<Talent> apiModels = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

                if (apiModels is null || !apiModels.Any()) return;

                List<Talent> localModels = _context.Talent.AsNoTracking().ToList();

                List<Talent> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<Talent> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncArmor()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewArmor)).DateTime;

                if (_apiSyncStatus.ArmorLastSync < lastLocalTransaction) return;

                List<Armor> apiModels = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

                if (apiModels is null || !apiModels.Any()) return;

                List<Armor> localModels = _context.Armor.AsNoTracking().ToList();

                List<Armor> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<Armor> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncGear()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewGear)).DateTime;

                if (_apiSyncStatus.GearLastSync < lastLocalTransaction) return;

                List<Gear> apiModels = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

                if (apiModels is null || !apiModels.Any()) return;

                List<Gear> localModels = _context.Gear.AsNoTracking().ToList();

                List<Gear> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<Gear> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncWeapons()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewWeapon)).DateTime;

                if (_apiSyncStatus.WeaponLastSync < lastLocalTransaction) return;

                List<Weapon> apiModels = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

                if (apiModels is null || !apiModels.Any()) return;

                List<Weapon> localModels = _context.Weapon.AsNoTracking().ToList();

                List<Weapon> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<Weapon> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncArchetypes()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewArchetype)).DateTime;

                if (_apiSyncStatus.ArchetypeLastSync < lastLocalTransaction) return;

                List<Archetype> apiModels = await GetRequestForListAsync<Archetype>(_route, _controller, "archtypeList");

                if (apiModels is null || !apiModels.Any()) return;

                List<Archetype> localModels = _context.Archetype.AsNoTracking().ToList();

                List<Archetype> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<Archetype> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncPyschic()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.AddNewPyschicPower)).DateTime;

                if (_apiSyncStatus.ArchetypeLastSync < lastLocalTransaction) return;

                List<PyschicPower> apiModels = await GetRequestForListAsync<PyschicPower>(_route, _controller, "pyschicList");

                if (apiModels is null || !apiModels.Any()) return;

                List<PyschicPower> localModels = _context.PsychicPowers.AsNoTracking().ToList();

                List<PyschicPower> newModels = GetNewApiModelsFromLocalModels(apiModels, localModels);
                List<PyschicPower> updatedModels = RemoveNewModelsFromApiModels(apiModels, newModels);

                _context.UpdateRange(updatedModels);
                _context.AddRange(newModels);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task SyncCharacters()
        {
            try
            {
                DateTime lastLocalTransaction = _context.Transactions
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault(x => x.SourceMethod == nameof(CharacterRepository.UpdateCharacter)).DateTime;

                if (_apiSyncStatus.CharacterLastSync < lastLocalTransaction) return;

                List<CharacterSync> characterSyncs = await GetRequestForListAsync<CharacterSync>(_route, _controller, "characterList");

                _context.ChangeTracker.Clear();

                foreach (CharacterSync characterSync in characterSyncs)
                {

                    if (!string.IsNullOrWhiteSpace(characterSync.Json))
                    {

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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
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
            try
            {

                List<CharacterSync> characterSyncs = await GetRequestForListAsync<CharacterSync>(_route, _controller, "characterList");

                _context.ChangeTracker.Clear();

                foreach (CharacterSync characterSync in characterSyncs)
                {

                    if (!string.IsNullOrWhiteSpace(characterSync.Json))
                    {

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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadArchetypes()
        {
            try
            {
                List<Archetype> models = await GetRequestForListAsync<Archetype>(_route, _controller, "archetypeList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadTalents()
        {
            try
            {
                List<Talent> models = await GetRequestForListAsync<Talent>(_route, _controller, "talentList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadArmor()
        {
            try
            {
                List<Armor> models = await GetRequestForListAsync<Armor>(_route, _controller, "armorList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadGear()
        {
            try
            {
                List<Gear> models = await GetRequestForListAsync<Gear>(_route, _controller, "gearList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadWeapons()
        {
            try
            {
                List<Weapon> models = await GetRequestForListAsync<Weapon>(_route, _controller, "weaponList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task LoadPyschic()
        {
            try
            {
                List<Weapon> models = await GetRequestForListAsync<Weapon>(_route, _controller, "pyschicList");

                if (models is null || !models.Any()) return;

                _context.AddRange(models);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
