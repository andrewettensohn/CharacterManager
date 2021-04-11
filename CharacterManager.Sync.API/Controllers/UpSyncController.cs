
using CharacterManager.DAC.Data;
using CharacterManager.Models;
using CharacterManager.Sync.API.Data;
using CharacterManager.Sync.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        private readonly IDbContextFactory<SyncDbContext> _dbFactory;


        public UpSyncController(IDbContextFactory<SyncDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        [HttpPost("characterList")]
        public IActionResult UpdateCharacterList(List<Character> characters)
        {
            try
            {
                List<CharacterModel> models = new List<CharacterModel>();
                foreach (Character character in characters)
                {
                    models.Add(new CharacterModel { CharacterId = character.CharacterId, CharacterJson = JObject.FromObject(character).ToString() });
                }

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    context.UpdateRange(characters);
                    context.SaveChanges();
                }

                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[HttpPost("transactionList")]
        //public async Task<IActionResult> AddTransactionList(List<Transaction> transactions)
        //{
        //    await CharacterRepository.AddTransactionList(transactions);

        //    return Ok();
        //}

        //[HttpGet("transactionList")]
        //public async Task<IActionResult> GetTransactionList()
        //{
        //    List<Transaction> transactions = await CharacterRepository.ListTransactions();

        //    return Ok(transactions);
        //}

        //[HttpPost("character")]
        //public async Task<IActionResult> UpdateCharacter(Character character)
        //{
        //    await CharacterRepository.UpdateCharacter(character);

            //return Ok();
        }

        //[HttpGet("characterList")]
        //public async Task<IActionResult> UpdateCharacterList()
        //{
        //    List<Character> characters = await CharacterRepository.ListCharacters();

        //    return Ok(characters);
        //}

        //[HttpPost("characterList")]
        //public async Task<IActionResult> UpdateCharacterList(List<Character> characters)
        //{
        //    await CharacterRepository.UpdateCharacterList(characters);

        //    return Ok();
        //}

    }
}
