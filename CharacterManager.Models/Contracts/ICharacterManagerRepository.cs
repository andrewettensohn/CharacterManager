using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models.Contracts
{
    public interface ICharacterManagerRepository
    {
        List<CoreModel> GetAll<CoreModel>(ModelType modelType);
        CoreModel GetById<CoreModel>(Guid id);
        void Update<CoreModel>(CoreModel coreModel);
        void UpdateSyncModels(List<SyncModel> syncModels);
        void DeleteById(Guid id);
    }
}
