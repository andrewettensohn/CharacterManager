using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class ConfigParam
    {
        public int ConfigParamId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime Time { get; set; }
    }
}
