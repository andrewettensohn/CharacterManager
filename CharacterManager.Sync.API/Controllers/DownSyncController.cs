using CharacterManager.DAC.Models;
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

        [HttpGet("isAvailable")]
        public IActionResult IsAvailable()
        {
            return Ok(true);
        }

        [HttpGet("syncStatus")]
        public IActionResult GetSyncStatus()
        {

            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                SyncStatus syncStatus = context.SyncStatus.FirstOrDefault();

                if (syncStatus == null)
                {
                    context.SyncStatus.Add(new SyncStatus());
                    context.SaveChanges();
                }

                return Ok(syncStatus);
            }
        }

        [HttpGet("characterList")]
        public IActionResult GetCharacterList()
        {
            try
            {
                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {
                    List<CharacterSync> result = context.CharacterModels.ToList();

                    List<Character> coreModelList = ConvertSyncModelsToCoreModels<Character, CharacterSync>(result);

                    return Ok(coreModelList);
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

                    List<Armor> coreModelList = ConvertSyncModelsToCoreModels<Armor, ArmorSync>(result);

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

                    List<Archetype> coreModelList = ConvertSyncModelsToCoreModels<Archetype, ArchetypeSync>(result);

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

                    List<Talent> coreModelList = ConvertSyncModelsToCoreModels<Talent, TalentSync>(result);

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

                    List<Gear> coreModelList = ConvertSyncModelsToCoreModels<Gear, GearSync>(result);

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

                    List<Weapon> coreModelList = ConvertSyncModelsToCoreModels<Weapon, WeaponSync>(result);

                    return Ok(coreModelList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private static List<CoreModel> ConvertSyncModelsToCoreModels<CoreModel, SyncModel>(List<SyncModel> syncModels)
            where CoreModel : ICoreCharacterModel
            where SyncModel : ICharacterManagerSync
        {
            List<string> stringModels = syncModels.Select(x => x.Json).ToList();

            List<CoreModel> coreModelList = new List<CoreModel>();

            foreach (string stringModel in stringModels)
            {
                JObject jsonCharacter = JObject.Parse(stringModel);
                CoreModel coreModel = jsonCharacter.ToObject<CoreModel>();
                coreModelList.Add(coreModel);
            }

            return coreModelList;
        }
    }
}
