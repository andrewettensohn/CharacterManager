using System.Collections.Generic;

namespace CharacterManager.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Stat> FeatureStatModifiers { get; set; }
    }
}
