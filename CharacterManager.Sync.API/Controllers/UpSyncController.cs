﻿using CharacterManager.DAC.Data;
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
        public IActionResult UpdateCharacterList(List<CharacterSync> characterSyncs)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<CharacterSync> allModels = context.CharacterModels.AsNoTracking().ToList();

                    foreach(CharacterSync characterSync in characterSyncs)
                    {
                        bool isNewCharacter = !allModels.Any(x => x.Id == characterSync.Id);

                        if (isNewCharacter)
                        {
                            context.CharacterModels.Add(characterSync);
                        }
                        else
                        {
                            context.CharacterModels.Update(characterSync);
                        }
                    }

                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.CharacterLastSync));

                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("armorList")]
        public IActionResult UpdateArmorList(List<Armor> armor)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<ArmorSync> allModels = context.ArmorModels.AsNoTracking().ToList();

                    List<ArmorSync> updatedModels = new List<ArmorSync>();
                    List<ArmorSync> newModels = new List<ArmorSync>();

                    Tuple<List<ArmorSync>, List<ArmorSync>> sortResult = SortNewAndUpdatedModels(allModels, armor);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.ArmorModels.AddRange(newModels);
                    context.ArmorModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.ArmorLastSync));

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("archetypeList")]
        public IActionResult UpdateArchetypeList(List<Archetype> archetypes)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<ArchetypeSync> allModels = context.ArchetypeModels.AsNoTracking().ToList();

                    List<ArchetypeSync> updatedModels = new List<ArchetypeSync>();
                    List<ArchetypeSync> newModels = new List<ArchetypeSync>();

                    Tuple<List<ArchetypeSync>, List<ArchetypeSync>> sortResult = SortNewAndUpdatedModels(allModels, archetypes);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.ArchetypeModels.AddRange(newModels);
                    context.ArchetypeModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.ArchetypeLastSync));

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("gearList")]
        public IActionResult UpdateGearList(List<Gear> gear)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<GearSync> allModels = context.GearModels.AsNoTracking().ToList();

                    List<GearSync> updatedModels = new List<GearSync>();
                    List<GearSync> newModels = new List<GearSync>();

                    Tuple<List<GearSync>, List<GearSync>> sortResult = SortNewAndUpdatedModels(allModels, gear);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.GearModels.AddRange(newModels);
                    context.GearModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.GearLastSync));

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("talentList")]
        public IActionResult UpdateTalentList(List<Talent> talents)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<TalentSync> allModels = context.TalentModels.AsNoTracking().ToList();

                    List<TalentSync> updatedModels = new List<TalentSync>();
                    List<TalentSync> newModels = new List<TalentSync>();

                    Tuple<List<TalentSync>, List<TalentSync>> sortResult = SortNewAndUpdatedModels(allModels, talents);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.TalentModels.AddRange(newModels);
                    context.TalentModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.TalentLastSync));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("weaponList")]
        public IActionResult UpdateWeaponList(List<Weapon> weapons)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<WeaponSync> allModels = context.WeaponModels.AsNoTracking().ToList();

                    List<WeaponSync> updatedModels = new List<WeaponSync>();
                    List<WeaponSync> newModels = new List<WeaponSync>();

                    Tuple<List<WeaponSync>, List<WeaponSync>> sortResult = SortNewAndUpdatedModels(allModels, weapons);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.WeaponModels.AddRange(newModels);
                    context.WeaponModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.WeaponLastSync));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("pyschicList")]
        public IActionResult UpdatePsychicList(List<PyschicPower> powers)
        {
            try
            {

                using (SyncDbContext context = _dbFactory.CreateDbContext())
                {

                    List<PyschicPowerSync> allModels = context.PsychicPowerModels.AsNoTracking().ToList();

                    List<PyschicPowerSync> updatedModels = new List<PyschicPowerSync>();
                    List<PyschicPowerSync> newModels = new List<PyschicPowerSync>();

                    Tuple<List<PyschicPowerSync>, List<PyschicPowerSync>> sortResult = SortNewAndUpdatedModels(allModels, powers);

                    newModels = sortResult.Item1;
                    updatedModels = sortResult.Item2;

                    context.PsychicPowerModels.AddRange(newModels);
                    context.PsychicPowerModels.UpdateRange(updatedModels);
                    context.SaveChanges();
                }

                UpdateSyncTime(nameof(SyncStatus.PsychicLastSync));

                return Ok();
            }
            catch (Exception ex)
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

        private void UpdateSyncTime(string syncName)
        {
            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                SyncStatus syncStatus = context.SyncStatus.FirstOrDefault();

                if (syncStatus == null)
                {
                    syncStatus = new SyncStatus();
                }

                syncStatus.GetType().GetProperty(syncName).SetValue(syncStatus, DateTime.UtcNow);

                context.SyncStatus.Update(syncStatus);
                context.SaveChanges();
            }
        }
    }
}
