using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CharacterManager.Sync
{
    public class Sync
    {
        private const string Route = "Sync";
        private ICharacterRepository _characterRepository;
        private ITransactionRepository _transactionRepository;

        public Sync(ICharacterRepository characterRepository, ITransactionRepository transactionRepository)
        {
            _characterRepository = characterRepository;
            _transactionRepository = transactionRepository;
        }

        [FunctionName("AddTransactionList")]
        public async Task<IActionResult> AddTransactionList(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = Route)] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<Transaction> newTransactions = JsonConvert.DeserializeObject<List<Transaction>>(requestBody);

            await _transactionRepository.AddTransactionList(newTransactions);

            return new OkResult();
        }

        [FunctionName("UpdateCharacter")]
        public async Task<IActionResult> NewCharacter(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = Route)] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Character newCharacter = JsonConvert.DeserializeObject<Character>(requestBody);

            await _characterRepository.UpdateCharacter(newCharacter);

            return new OkResult();
        }
    }
}

