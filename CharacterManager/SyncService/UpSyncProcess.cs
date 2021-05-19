using CharacterManager.Models;
using CharacterManager.Models.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharacterManager.SyncService
{
    public class UpSyncProcess : BaseSyncProcess
    {
        private readonly ICharacterManagerRepository _repository;
        private readonly ILogger<DownSyncProcess> _logger;
        private readonly string _route;
        private readonly string _controller = "upSync";

        public UpSyncProcess(HttpClient http, IServiceScopeFactory scopeFactory) : base(http)
        {
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                _repository = scope.ServiceProvider.GetRequiredService<ICharacterManagerRepository>();
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<DownSyncProcess>>();

                IHostEnvironment hostEnv = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
                IConfiguration config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                if (hostEnv.IsDevelopment())
                {
                    _route = $"{config["Routes:Dev"]}";
                }
                else
                {
                    _route = $"{config["Routes:Prod"]}";
                }
            }
        }

        public async Task ExecuteAsync()
        {

            List<SyncModel> localSyncModels = _repository.GetSyncModelsChangedSinceLastUpSyncTime();

            if (!localSyncModels.Any()) return;

            HttpResponseMessage response = await PostRequestForResponse(localSyncModels, _route, _controller, "downSync");

            if(response.IsSuccessStatusCode)
            {
                _repository.UpdateLastUpSyncTimeToNow();
            }
        }
    }
}
