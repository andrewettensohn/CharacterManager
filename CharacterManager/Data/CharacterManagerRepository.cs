using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Models.Contracts;
using CharacterManager.Models.Extensions;
using CharacterManager.Sync.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class CharacterManagerRepository : ICharacterManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterManagerRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public List<CoreModel> GetAllCoreModelsForModelType<CoreModel>(ModelType modelType) where CoreModel : ICoreCharacterModel
        {
            List<SyncModel> syncModels = _context.SyncModels.Where(x => x.ModelType == modelType).ToList();

            List<CoreModel> coreModels = syncModels.ConvertSyncModelsToCoreModels<CoreModel>();

            return coreModels;
        }

        public List<SyncModel> GetAllSyncModels()
        {
            List<SyncModel> syncModels = _context.SyncModels.ToList();

            return syncModels;
        }

        public CoreModel GetCoreModelById<CoreModel>(Guid id) where CoreModel : ICoreCharacterModel
        {
            SyncModel syncModel = _context.SyncModels.Find(id);

            CoreModel coreModel = syncModel.ConvertSyncModelToCoreModel<CoreModel>();

            return coreModel;
        }

        public CoreModel AddCoreModel<CoreModel>(CoreModel coreModel, ModelType modelType) where CoreModel : ICoreCharacterModel
        {
            SyncModel newModel = new SyncModel
            {
                Id = coreModel.Id,
                Json = JsonConvert.SerializeObject(coreModel),
                LastUpdateDateTime = DateTime.Now,
                ModelType = modelType
            };

            _context.SyncModels.Add(newModel);
            _context.SaveChanges();

            coreModel.Id = newModel.Id;
            return coreModel;
        }

        public void UpdateCoreModel<CoreModel>(CoreModel coreModel) where CoreModel : ICoreCharacterModel
        {
            SyncModel syncModel = _context.SyncModels.Find(coreModel.Id);
            syncModel.LastUpdateDateTime = DateTime.Now;
            syncModel.Json = JsonConvert.SerializeObject(coreModel);

            _context.SyncModels.Update(syncModel);
            _context.SaveChanges();
        }

        public List<SyncModel> GetSyncModelsChangedSinceLastUpSyncTime()
        {
            SyncStatus syncStatus = GetSyncStatus();

            List<SyncModel> syncModels = _context.SyncModels.Where(x => x.LastUpdateDateTime >= syncStatus.LastUpSyncDateTime).ToList();

            return syncModels;
        }

        public void UpdateSyncModels(List<SyncModel> syncModels)
        {
            List<Guid> apiIds = _context.SyncModels.AsNoTracking().Select(x => x.Id).ToList();

            List<SyncModel> updatedModels = syncModels.Where(x => apiIds.Any(o => o == x.Id)).ToList();
            List<SyncModel> newModels = syncModels.Where(x => !apiIds.Any(o => o == x.Id)).ToList();

            _context.UpdateRange(updatedModels);
            _context.AddRange(newModels);

            _context.SaveChanges();

        }

        public void DeleteSyncModelById(Guid id)
        {
            SyncModel syncModel = _context.SyncModels.FirstOrDefault(x => x.Id == id);
            
            _context.SyncModels.Remove(syncModel);
            _context.SaveChanges();
        }

        public void UpdateLastUpSyncTimeToNow()
        {
            SyncStatus status = _context.SyncStatus.FirstOrDefault();

            if(status is null)
            {
                SyncStatus newStatus = new SyncStatus
                {
                    LastUpSyncDateTime = DateTime.Now,
                    LastDownSyncDateTime = DateTime.MinValue,
                };

                _context.Add(newStatus);
            }
            else
            {
                status.LastUpSyncDateTime = DateTime.Now;

                _context.SyncStatus.Update(status);
            }

            _context.SaveChanges();
        }

        public void UpdateLastDownSyncTimeToNow()
        {
            SyncStatus status = _context.SyncStatus.FirstOrDefault();

            if (status is null)
            {
                SyncStatus newStatus = new SyncStatus
                {
                    LastUpSyncDateTime = DateTime.MinValue,
                    LastDownSyncDateTime = DateTime.Now,
                };

                _context.Add(newStatus);
            }
            else
            {
                status.LastDownSyncDateTime = DateTime.Now;

                _context.SyncStatus.Update(status);
            }

            _context.SaveChanges();
        }

        public SyncStatus GetSyncStatus()
        {

            SyncStatus status = _context.SyncStatus.FirstOrDefault();

            if (status is null)
            {
                SyncStatus newStatus = new SyncStatus
                {
                    LastUpSyncDateTime = DateTime.MinValue,
                    LastDownSyncDateTime = DateTime.MinValue,
                };

                _context.Add(newStatus);
                _context.SaveChanges();
            }

            return status;
        }

    }
}
