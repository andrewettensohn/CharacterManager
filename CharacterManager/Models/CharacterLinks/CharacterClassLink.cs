﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models.CharacterLinks
{
    public class CharacterClassLink
    {
        public int CharacterClassLinkId { get; set; }

        public int CharacterId { get; set; }

        public int CharacterClassId { get; set; }
    }
}