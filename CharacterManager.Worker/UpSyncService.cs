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
    public class UpSyncService : IHostedService, IDisposable
    {
        private readonly ILogger<UpSyncService> _logger;
        private Timer _timer;
        private readonly IConfiguration _config;
        private readonly UpSyncRestClient _restClient;

        public UpSyncService(ILogger<UpSyncService> logger, IConfiguration config, UpSyncRestClient restClient)
        {
            _config = config ?? throw new ArgumentNullException();
            _logger = logger;
            _restClient = restClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sync Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Syncing.");
            try
            {

                await _restClient.ExecuteUpSync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Sync Failed");
            }
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
