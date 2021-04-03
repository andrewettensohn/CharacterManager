using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Weapon
    {
        public Guid WeaponId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }

        public int ED { get; set; }

        public int AP { get; set; }

        public string Salvo { get; set; }

        public string Range { get; set; }

        public string Traits { get; set; }

        public bool IsMelee { get; set; }

        public bool IsEquipped { get; set; }

    }
}
