using CharacterManager.DAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models.Contracts
{
    public interface ICharacterManagerRepository
    {
        List<CoreModel> GetAllCoreModelsForModelType<CoreModel>(ModelType modelType) where CoreModel : ICoreCharacterModel;
        List<CoreModel> GetAllCoreModels<CoreModel>() where CoreModel : ICoreCharacterModel;
        CoreModel GetCoreModelById<CoreModel>(Guid id) where CoreModel : ICoreCharacterModel;
        CoreModel AddCoreModel<CoreModel>(CoreModel coreModel, ModelType modelType) where CoreModel : ICoreCharacterModel;
        void UpdateCoreModel<CoreModel>(CoreModel coreModel) where CoreModel : ICoreCharacterModel;
        List<SyncModel> GetSyncModelsChangedSinceLastUpSyncTime();
        void UpdateSyncModels(List<SyncModel> syncModels);
        void DeleteSyncModelById(Guid id);
        void UpdateLastUpSyncTimeToNow();
        void UpdateLastDownSyncTimeToNow();
        SyncStatus GetSyncStatus();
    }
}
