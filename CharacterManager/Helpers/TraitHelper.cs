using CharacterManager.UserMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace CharacterManager.Helpers
{
    public class TraitHelper
    {
        public static Dictionary<string, string> GetUserMessagesForTraitType(string traits, Type traitType)
        {
            Dictionary<string, string> messageDictionary = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(traits)) return messageDictionary;

            IEnumerable<FieldInfo> messageProperties = traitType.GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => traits.Contains(x.Name));

            foreach (FieldInfo messageProperty in messageProperties)
            {
                messageDictionary.Add(messageProperty.Name, messageProperty.GetValue(messageProperty).ToString());
            }

            return messageDictionary;
        }
    }
}
