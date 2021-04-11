using CharacterManager.Models;
using CharacterManager.Sync.API.Data;
using CharacterManager.Sync.API.Models;
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
    [Route("downSync")]
    public class DownSyncController : ControllerBase
    {

        private readonly IDbContextFactory<SyncDbContext> _dbFactory;


        public DownSyncController(IDbContextFactory<SyncDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }


        [HttpGet("characterList")]
        public IActionResult GetCharacterList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<CharacterSync> result = context.CharacterModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Character> characterList = new List<Character>();

                    foreach(string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Character character = jsonCharacter.ToObject<Character>();
                        characterList.Add(character);
                    }

                    return Ok(characterList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
