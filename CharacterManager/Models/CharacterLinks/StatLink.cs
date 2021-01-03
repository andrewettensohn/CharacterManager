using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class StatLink
    {
        public int StatLinkId { get; set; }

        public int CharacterId { get; set; }

        public int? StatId { get; set; }

        public int? EquipmentId { get; set; }

        public int? FeatureId { get; set; }

        public int? RaceId { get; set; }

        public int? CharacterClassId { get; set; }
    }
}
