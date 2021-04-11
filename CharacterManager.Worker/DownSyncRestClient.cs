using CharacterManager.DAC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly string _route;
        private readonly string _controller = "downSync";

        public DownSyncRestClient(HttpClient http, IConfiguration config, IHostEnvironment env, IServiceScopeFactory serviceScopeFactory) : base(http)
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
            bool isDownSyncApiAvailable = await IsDownSyncApiAvailable();

            if (!isDownSyncApiAvailable) return;

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                _characterRepository = scope.ServiceProvider.GetRequiredService<ICharacterRepository>();

                //await SyncTransactions();
                //await SyncCharacters();
                //await SyncArchetypes();
                //await SyncArmor();
                //await SyncGear();
                //await SyncTalent();
                //await SyncWeapons();
            }
        }

        private async Task<bool> IsDownSyncApiAvailable()
        {
            HttpResponseMessage response = await GetContent(_route, "downSync", "isAvailable");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        //private async Task SyncCharacters()
        //{

        //}
    }
}
