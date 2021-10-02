using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Sync.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Controllers
{
    [ApiController]
    [Route("sync")]
    public class SyncController : ControllerBase
    {
        private readonly IDbContextFactory<SyncDbContext> _dbFactory;

        public SyncController(IDbContextFactory<SyncDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        [HttpGet("syncModels/{dateTime}")]
        public IActionResult GetSyncModelsSinceLastSyncTime(DateTime dateTime)
        {

            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                List<SyncModel> syncModels = context.SyncModels.Where(x => x.LastUpdateDateTime >= dateTime).ToList();

                return Ok(syncModels);
            }
        }

        [HttpGet("syncModels")]
        public IActionResult GetSyncModels()
        {

            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                List<SyncModel> syncModels = context.SyncModels.ToList();

                return Ok(syncModels);
            }
        }

        [HttpPost("syncModels")]
        public IActionResult PostSyncModels(List<SyncModel> syncModels)
        {
            syncModels.ForEach(x =>
            {
                if(x.Id == Guid.Empty)
                {
                    x.Id = Guid.NewGuid();
                }
            });

            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                List<Guid> apiIds = context.SyncModels.AsNoTracking().Select(x => x.Id).ToList();

                List<SyncModel> updatedModels = syncModels.Where(x => apiIds.Any(o => o == x.Id)).ToList();
                List<SyncModel> newModels = syncModels.Where(x => !apiIds.Any(o => o == x.Id)).ToList();

                context.UpdateRange(updatedModels);
                context.AddRange(newModels);

                context.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete("syncModels/{id}")]
        public IActionResult GetSyncModelsSinceLastSyncTime(Guid id)
        {

            using (SyncDbContext context = _dbFactory.CreateDbContext())
            {
                SyncModel syncModel = context.SyncModels.Find(id);
                context.Remove(syncModel);

                context.SaveChanges();

                return Ok();
            }
        }

    }
}
