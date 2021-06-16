using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
using CharacterManager.Data;
using CharacterManager.Models;
using CharacterManager.Models.Contracts;
using Microsoft.EntityFrameworkCore;
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
    public class DownSyncProcess : BaseSyncProcess
    {
        private readonly ICharacterManagerRepository _repository;
        private readonly ILogger<DownSyncProcess> _logger;
        private readonly string _route;
        private readonly string _controller = "sync";

        public DownSyncProcess(HttpClient http, IServiceScopeFactory scopeFactory) : base(http)
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
            SyncStatus status = _repository.GetSyncStatus();

            List<SyncModel> apiSyncModels = await GetRequestForListAsync<SyncModel>(_route, _controller, $"syncModels/{status.LastDownSyncDateTime:yyyy-MM-dd HH:mm:ss}");

            if (apiSyncModels is null || !apiSyncModels.Any()) return;

            _repository.UpdateSyncModels(apiSyncModels);

            _repository.UpdateLastDownSyncTimeToNow();
        }
    }
}
