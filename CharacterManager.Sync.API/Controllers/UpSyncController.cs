
using CharacterManager.DAC.Data;
using CharacterManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Controllers
{
    [ApiController]
    [Route("upSync")]
    public class UpSyncController : ControllerBase
    {
        private readonly ICharacterRepository CharacterRepository;

        public UpSyncController(ICharacterRepository characterRepository)
        {
            CharacterRepository = characterRepository;
        }

        [HttpPost("transactionList")]
        public async Task<IActionResult> AddTransactionList(List<Transaction> transactions)
        {
            await CharacterRepository.AddTransactionList(transactions);

            return Ok();
        }

        [HttpGet("transactionList")]
        public async Task<IActionResult> GetTransactionList()
        {
            List<Transaction> transactions = await CharacterRepository.ListTransactions();

            return Ok(transactions);
        }

        [HttpPost("character")]
        public async Task<IActionResult> UpdateCharacter(Character character)
        {
            await CharacterRepository.UpdateCharacter(character);

            return Ok();
        }

        [HttpGet("characterList")]
        public async Task<IActionResult> UpdateCharacterList()
        {
            List<Character> characters = await CharacterRepository.ListCharacters();

            return Ok(characters);
        }

        [HttpPost("characterList")]
        public async Task<IActionResult> UpdateCharacterList(List<Character> characters)
        {
            await CharacterRepository.UpdateCharacterList(characters);

            return Ok();
        }

    }
}
