
using CharacterManager.DAC.Data;
using CharacterManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ICharacterRepository CharacterRepository;

        public SyncController(ICharacterRepository characterRepository)
        {
            CharacterRepository = characterRepository;
        }

        [HttpPost("character")]
        public async Task<IActionResult> UpdateCharacter(Character character)
        {
            await CharacterRepository.UpdateCharacter(character);

            return Ok();
        }

        
    }
}
