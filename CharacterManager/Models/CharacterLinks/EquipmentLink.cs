﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class EquipmentLink
    {
        public int EquipmentLinkId { get; set; }

        public int EquipmentId { get; set; }

        public int CharacterId { get; set; }
    }
}
