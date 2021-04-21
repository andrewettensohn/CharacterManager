using CharacterManager.DAC.Models;
using CharacterManager.Sync.API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Models.Extensions
{
    public static class Extensions
    {
        public static CoreModel ConvertSyncModelToCoreModel<CoreModel, SyncModel>(this SyncModel syncModel)
            where CoreModel : ICoreCharacterModel
            where SyncModel : ICharacterManagerSync
        {

            JObject jsonCharacter = JObject.Parse(syncModel.Json);
            CoreModel coreModel = jsonCharacter.ToObject<CoreModel>();

            return coreModel;
        }

        public static List<CoreModel> ConvertSyncModelsToCoreModels<CoreModel, SyncModel>(this List<SyncModel> syncModels)
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
