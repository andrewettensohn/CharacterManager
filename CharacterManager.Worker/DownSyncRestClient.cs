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
        private ICharacterRepository _characterRepository;
        private ApplicationDbContext _context;
        private readonly string _route;
        private readonly string _controller = "downSync";

        public DownSyncRestClient(ILogger<DownSyncRestClient> logger, HttpClient http, IConfiguration config, IHostEnvironment env, IServiceScopeFactory serviceScopeFactory) : base(http)
        {
            _config = config ?? throw new ArgumentNullException();
            _scopeFactory = serviceScopeFactory;

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

        public async Task ExecuteUpSync()
        {
            bool isDownSyncApiAvailable = await IsDownSyncApiAvailable();

            if (!isDownSyncApiAvailable) return;

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                _characterRepository = scope.ServiceProvider.GetRequiredService<ICharacterRepository>();
                IDbContextFactory<ApplicationDbContext> dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
                _context = dbFactory.CreateDbContext();
                //SyncStatus syncStatus = await _context.SyncStatus.FirstOrDefaultAsync();
                bool areCharactersPresent = _context.Character.Any();

                if (!areCharactersPresent)
                {
                    await PerformInitalLoad();
                }
            }
        }

        private async Task<bool> IsDownSyncApiAvailable()
        {
            HttpResponseMessage response = await GetContent(_route, _controller, "isAvailable");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task PerformInitalLoad()
        {
            await LoadCharacters();
            await LoadArchetypes();
            await LoadTalents();
            await LoadArmor();
            await LoadGear();
            await LoadWeapons();
        }

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

        //private async Task SyncCharacters()
        //{

        //}
    }
}
