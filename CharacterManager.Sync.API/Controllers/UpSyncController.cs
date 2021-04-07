
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

        [HttpGet("characterList")]
        public async Task<IActionResult> GetCharacterList()
        {
            List<Character> characters = await CharacterRepository.ListCharacters();

            return Ok(characters);
        }

        [HttpPost("characterList")]
        public async Task<IActionResult> UpdateCharacterList(List<Character> updatedCharacters)
        {

            List<Character> allCharacters = await CharacterRepository.ListCharacters();

            List<Character> newCharacters = updatedCharacters.Where(character => !allCharacters.Any(x => x.CharacterId == character.CharacterId)).ToList();

            updatedCharacters.RemoveAll(character => newCharacters.Any(x => x.CharacterId == character.CharacterId));

            foreach (Character character in updatedCharacters)
            {
                await CharacterRepository.SyncUpdateCharacter(character);
            }

            foreach (Character character in newCharacters)
            {
                await CharacterRepository.SyncNewCharacter(character);
            }

            return Ok();
        }

        [HttpGet("armorList")]
        public async Task<IActionResult> GetArmorList()
        {
            List<Armor> armor = await CharacterRepository.GetArmorList();

            return Ok(armor);
        }

        [HttpPost("armorList")]
        public async Task<IActionResult> UpdateArmorList(List<Armor> armor)
        {
            await CharacterRepository.UpdateArmorList(armor);

            return Ok();
        }

        [HttpGet("archetypeList")]
        public async Task<IActionResult> GetArchetypeList()
        {
            List<Archetype> archetypes = await CharacterRepository.GetArchetypes();

            return Ok(archetypes);
        }

        [HttpPost("archetypeList")]
        public async Task<IActionResult> UpdateArchetypeList(List<Archetype> archetypes)
        {
            await CharacterRepository.UpdateArchetypeList(archetypes);

            return Ok();
        }

        [HttpGet("gearList")]
        public async Task<IActionResult> GetGearList()
        {
            List<Gear> gear = await CharacterRepository.GetGearList();

            return Ok(gear);
        }

        [HttpPost("gearList")]
        public async Task<IActionResult> UpdateGearList(List<Gear> gear)
        {
            await CharacterRepository.UpdateGearList(gear);

            return Ok();
        }

        [HttpGet("talentList")]
        public async Task<IActionResult> GetTalentList()
        {
            List<Talent> talents = await CharacterRepository.GetTalents();

            return Ok(talents);
        }

        [HttpPost("talentList")]
        public async Task<IActionResult> UpdateTalentList(List<Talent> talents)
        {
            await CharacterRepository.UpdateTalentList(talents);

            return Ok();
        }

        [HttpGet("weaponList")]
        public async Task<IActionResult> GetWeaponList()
        {
            List<Weapon> weapons = await CharacterRepository.GetWeapons();

            return Ok(weapons);
        }

        [HttpPost("weaponList")]
        public async Task<IActionResult> UpdateWeaponList(List<Weapon> weapons)
        {
            await CharacterRepository.UpdateWeaponList(weapons);

            return Ok();
        }

    }
}
