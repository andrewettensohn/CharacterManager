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
        
        public DateTime LastDownSyncDateTime { get; set; }

        public DateTime LastUpSyncDateTime { get; set; }
    }
}
