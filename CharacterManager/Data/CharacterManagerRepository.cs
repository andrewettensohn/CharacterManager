using CharacterManager.DAC.Models;
using CharacterManager.Models;
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
    public class CharacterManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterManagerRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public List<CoreModel> GetAll<CoreModel>(ModelType modelType) where CoreModel : ICoreCharacterModel
        {
            List<SyncModel> syncModels = _context.SyncModels.Where(x => x.ModelType == modelType).ToList();

            List<CoreModel> coreModels = syncModels.ConvertSyncModelsToCoreModels<CoreModel>();

            return coreModels;
        }

        public CoreModel GetById<CoreModel>(Guid id) where CoreModel : ICoreCharacterModel
        {
            SyncModel syncModel = _context.SyncModels.Find(id);

            CoreModel coreModel = syncModel.ConvertSyncModelToCoreModel<CoreModel>();

            return coreModel;
        }

        public void Update<CoreModel>(CoreModel coreModel) where CoreModel : ICoreCharacterModel
        {
            SyncModel syncModel = _context.SyncModels.Find(coreModel.Id);
            syncModel.Json = JsonConvert.SerializeObject(coreModel);

            _context.SyncModels.Update(syncModel);
            _context.SaveChanges();
        }

        public void UpdateSyncModels(List<SyncModel> syncModels)
        {
            _context.SyncModels.UpdateRange(syncModels);
            _context.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            SyncModel syncModel = _context.SyncModels.FirstOrDefault(x => x.Id == id);
            
            _context.SyncModels.Remove(syncModel);
            _context.SaveChanges();
        }

    }
}
