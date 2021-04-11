
using CharacterManager.DAC.Data;
using CharacterManager.DAC.Models;
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

        [HttpGet("isAvailable")]
        public IActionResult IsAvailable()
        {
            return Ok(true);
        }

        [HttpPost("characterList")]
        public IActionResult UpdateCharacterList(List<Character> characters)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<CharacterSync> allModels = context.CharacterModels.AsNoTracking().ToList();

                    List<CharacterSync> updatedModels = new List<CharacterSync>();
                    List<CharacterSync> newModels = new List<CharacterSync>();

                   Tuple<List<CharacterSync>, List<CharacterSync>> sortResult =  SortNewAndUpdatedModels(allModels, characters);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;


                    //foreach (Character character in characters)
                    //{
                    //    CharacterSync model = new CharacterSync { Id = character.Id, Json = JObject.FromObject(character).ToString() };

                    //    bool isNew = !allModels.Any(x => x.Id == model.CharacterId);

                    //    if(isNew)
                    //    {
                    //        newModels.Add(model);
                    //    }
                    //    else
                    //    {
                    //        updatedModels.Add(model);
                    //    }
                    //}

                    context.CharacterModels.AddRange(newModels);
                    context.CharacterModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private Tuple<List<SyncModel>, List<SyncModel>> SortNewAndUpdatedModels<SyncModel, SentModel>(List<SyncModel> allSyncModels, List<SentModel> sentModels)
            where SyncModel : ICharacterManagerSync
            where SentModel : ICoreCharacterModel
        {
            List<SyncModel> updatedModels = new List<SyncModel>();
            List<SyncModel> newModels = new List<SyncModel>();

            foreach (SentModel sentModel in sentModels)
            {
                SyncModel syncModel = Activator.CreateInstance<SyncModel>();
                syncModel.Id = sentModel.Id;
                syncModel.Json = JObject.FromObject(sentModel).ToString();

                bool isNew = !allSyncModels.Any(x => x.Id == syncModel.Id);

                if (isNew)
                {
                    newModels.Add(syncModel);
                }
                else
                {
                    updatedModels.Add(syncModel);
                }
            }

            return new Tuple<List<SyncModel>, List<SyncModel>>(newModels, updatedModels);
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

    //}
}
