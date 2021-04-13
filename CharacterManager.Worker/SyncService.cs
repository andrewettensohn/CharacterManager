using CharacterManager.DAC.Data;
using CharacterManager.Models;
using Microsoft.Extensions.Configuration;
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
        private readonly UpSyncRestClient _upRestClient;
        private readonly DownSyncRestClient _downRestClient;

        public SyncService(ILogger<SyncService> logger, IConfiguration config, UpSyncRestClient upsync, DownSyncRestClient downSync)
        {
            _config = config ?? throw new ArgumentNullException();
            _logger = logger;
            _upRestClient = upsync;
            _downRestClient = downSync;
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
                _logger.LogInformation("Starting down sync.");
                await _downRestClient.ExecuteUpSync();
                _logger.LogInformation("Finished down sync.");

                _logger.LogInformation("Starting up sync.");
                await _upRestClient.ExecuteUpSync();
                _logger.LogInformation("Finished up sync.");
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
