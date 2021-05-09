using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Models
{
    public class SyncStatus
    {
        [Key]
        public int Id { get; set; }
        public bool IsDownSyncStatus { get; set; }
        public DateTime CharacterLastSync { get; set; }
        public DateTime ArchetypeLastSync { get; set; }
        public DateTime ArmorLastSync { get; set; }
        public DateTime GearLastSync { get; set; }
        public DateTime TalentLastSync { get; set; }
        public DateTime WeaponLastSync { get; set; }
        public DateTime PsychicLastSync { get; set; }
        public DateTime QuestLastSync { get; set; }
    }
}
