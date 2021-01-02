using System.Collections.Generic;

namespace CharacterManager.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }

        public bool IsActive { get; set; }

        public int Weight { get; set; }

        public int Qty { get; set; }

        public int Cost { get; set; }

        public string Notes { get; set; }

        public ICollection<Stat> EquipmentStatModifiers { get; set; }
    }
}
