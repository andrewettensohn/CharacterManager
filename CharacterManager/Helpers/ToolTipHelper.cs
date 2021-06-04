using CharacterManager.UserMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CharacterManager.Helpers
{
    public class ToolTipHelper
    {
        public static string GetToolTipByPropNameAndType(string propName)
        {
            string tooltipText = string.Empty;

            if (string.IsNullOrWhiteSpace(propName)) return tooltipText;

            tooltipText = typeof(Tooltips).GetFields(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(x => x.Name == propName).GetValue(typeof(Tooltips))?.ToString();

            return tooltipText;
        }
    }
}
