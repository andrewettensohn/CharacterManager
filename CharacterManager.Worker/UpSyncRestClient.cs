using CharacterManager.DAC.Data;
using CharacterManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CharacterManager.Worker
{
    public class UpSyncRestClient : BaseRestClient
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private ICharacterRepository _characterRepository;
        private readonly string _route;
        private readonly string _controller = "upSync";

        public UpSyncRestClient(HttpClient http, IConfiguration config, IHostEnvironment env, IServiceScopeFactory serviceScopeFactory) : base(http)
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
            using(IServiceScope scope = _scopeFactory.CreateScope())
            {
                _characterRepository = scope.ServiceProvider.GetRequiredService<ICharacterRepository>();

                DateTime lastSyncTime = await _characterRepository.GetLastSyncTime();

                List<Transaction> newTransactions = await _characterRepository.GetTransactionsAfterLastSyncTime(lastSyncTime);

                if (!newTransactions.Any()) return;

                await SyncTransactions(newTransactions);

                List<Guid> characterTransactionIds = newTransactions.Select(x => x.SourceId).ToList();

                if (characterTransactionIds.Any())
                {
                    await SyncCharacters(characterTransactionIds);
                }
            }
        }

        public async Task SyncCharacters(List<Guid> ids)
        {
            List<Character> allCharacters = await _characterRepository.ListCharacters();

            List<Character> updatedCharacters = allCharacters.Where(x => ids.Any(id => id == x.CharacterId)).ToList();

            await PostContent(updatedCharacters, _route, _controller, "characterList");
        }

        public async Task SyncTransactions(List<Transaction> transactions)
        {

            HttpResponseMessage response = await PostContent(transactions, _route, "upSync", "transactionList");

            if (response.IsSuccessStatusCode)
            {
                await _characterRepository.UpdateSyncTime();
            }
        }

    }
}
