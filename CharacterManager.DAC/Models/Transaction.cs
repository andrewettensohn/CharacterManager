using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public string SourceRepository { get; set; }

        public string SourceMethod { get; set; }

        public Guid SourceId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
