using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class FeatureLink
    {
        public int FeatureLinkId { get; set; }

        public int CharacterId { get; set; }

        public int FeatureId { get; set; }
    }
}
