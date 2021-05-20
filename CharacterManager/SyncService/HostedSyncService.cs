using CharacterManager.DAC.Data;
using CharacterManager.Data;
using CharacterManager.Models;
using CharacterManager.SyncService;
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
    public class HostedSyncService : IHostedService, IDisposable
    {
        private readonly ILogger<HostedSyncService> _logger;
        private Timer _timer;
        private readonly IConfiguration _config;
        private IServiceScopeFactory _scopeFactory;

        public HostedSyncService(ILogger<HostedSyncService> logger, IConfiguration config, IServiceScopeFactory serviceScopeFactory)
        {
            _config = config ?? throw new ArgumentNullException();
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sync Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

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

                    DownSyncProcess downProcess = new DownSyncProcess(http, _scopeFactory);
                    UpSyncProcess upSyncProcess = new UpSyncProcess(http, _scopeFactory);

                    _logger.LogInformation("Starting up sync.");
                    await upSyncProcess.ExecuteAsync();
                    _logger.LogInformation("Finished up sync.");

                    _logger.LogInformation("Starting down sync.");
                    await downProcess.ExecuteAsync();
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
