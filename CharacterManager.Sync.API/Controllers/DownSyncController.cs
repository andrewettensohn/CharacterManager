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

        [HttpGet("armorList")]
        public IActionResult GetArmorList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<ArmorSync> result = context.ArmorModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Armor> coreModelList = new List<Armor>();

                    foreach (string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Armor coreModel = jsonCharacter.ToObject<Armor>();
                        coreModelList.Add(coreModel);
                    }

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("archetypeList")]
        public IActionResult GetArchetypeList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<ArchetypeSync> result = context.ArchetypeModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Archetype> coreModelList = new List<Archetype>();

                    foreach (string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Archetype coreModel = jsonCharacter.ToObject<Archetype>();
                        coreModelList.Add(coreModel);
                    }

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("talentList")]
        public IActionResult GetTalentList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<TalentSync> result = context.TalentModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Talent> coreModelList = new List<Talent>();

                    foreach (string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Talent coreModel = jsonCharacter.ToObject<Talent>();
                        coreModelList.Add(coreModel);
                    }

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("gearList")]
        public IActionResult GetGearList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<GearSync> result = context.GearModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Gear> coreModelList = new List<Gear>();

                    foreach (string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Gear coreModel = jsonCharacter.ToObject<Gear>();
                        coreModelList.Add(coreModel);
                    }

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("weaponList")]
        public IActionResult GetWeaponList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<WeaponSync> result = context.WeaponModels.ToList();

                    List<string> stringModels = result.Select(x => x.Json).ToList();

                    List<Weapon> coreModelList = new List<Weapon>();

                    foreach (string stringModel in stringModels)
                    {
                        JObject jsonCharacter = JObject.Parse(stringModel);
                        Weapon coreModel = jsonCharacter.ToObject<Weapon>();
                        coreModelList.Add(coreModel);
                    }

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
