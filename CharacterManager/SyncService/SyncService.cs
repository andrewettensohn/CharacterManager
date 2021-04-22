using CharacterManager.DAC.Data;
using CharacterManager.Data;
using CharacterManager.Models;
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
using System.Threading;
using System.Threading.Tasks;

namespace CharacterManager.Worker
{
    public class SyncService : IHostedService, IDisposable
    {
        private readonly ILogger<SyncService> _logger;
        private Timer _timer;
        private readonly IConfiguration _config;
        private IServiceScopeFactory _scopeFactory;

        public SyncService(ILogger<SyncService> logger, IConfiguration config, IServiceScopeFactory serviceScopeFactory)
        {
            _config = config ?? throw new ArgumentNullException();
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sync Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Running Sync Service.");
            try
            {
                using(IServiceScope scope = _scopeFactory.CreateScope())
                {
                    HttpClient http = scope.ServiceProvider.GetRequiredService<HttpClient>();
                    IHostEnvironment hostEnv = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
                    IDbContextFactory<ApplicationDbContext> dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
                    ICharacterRepository characterRepo = scope.ServiceProvider.GetRequiredService<ICharacterRepository>();
                    ILogger<UpSyncRestClient> upLogger = scope.ServiceProvider.GetRequiredService<ILogger<UpSyncRestClient>>();
                    ILogger<DownSyncRestClient> downLogger = scope.ServiceProvider.GetRequiredService<ILogger<DownSyncRestClient>>();

                    UpSyncRestClient upClient = new UpSyncRestClient(upLogger, http, _config, hostEnv, dbFactory, characterRepo);
                    DownSyncRestClient downClient = new DownSyncRestClient(downLogger, http, _config, hostEnv, dbFactory);

                    _logger.LogInformation("Starting up sync.");
                    await upClient.ExecuteUpSync();
                    _logger.LogInformation("Finished up sync.");

                    _logger.LogInformation("Starting down sync.");
                    await downClient.ExecuteDownSync();
                    _logger.LogInformation("Finished down sync.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Sync Failed: {ex}");
            }
            _logger.LogInformation("Finished Syncing.");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Sync Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
