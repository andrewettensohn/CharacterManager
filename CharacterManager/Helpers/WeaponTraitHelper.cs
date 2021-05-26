using CharacterManager.UserMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace CharacterManager.Helpers
{
    public class WeaponTraitHelper
    {
        public static Dictionary<string, string> GetWeaponTraitUserMessages(string traits)
        {
            Dictionary<string, string> messageDictionary = new Dictionary<string, string>();

            IEnumerable<FieldInfo> messageProperties = typeof(WeaponTraitUserMessages).GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => traits.Contains(x.Name));

            foreach (FieldInfo messageProperty in messageProperties)
            {
                messageDictionary.Add(messageProperty.Name, messageProperty.GetValue(messageProperty).ToString());
            }

            return messageDictionary;
        }
    }
}
