using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Models.Links;
using CharacterManager.Sync.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Data
{
    public class SyncDbContext : DbContext
    {
        public SyncDbContext(DbContextOptions<SyncDbContext> options)
            : base(options) { }

        public DbSet<SyncModel> SyncModels { get; set; }

    }
}
