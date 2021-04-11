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

        public DbSet<CharacterSync> CharacterModels { get; set; }
        public DbSet<ArmorSync> ArmorModels { get; set; }
        public DbSet<GearSync> GearModels { get; set; }
        public DbSet<TalentSync> TalentModels { get; set; }
        public DbSet<WeaponSync> WeaponModels { get; set; }
        public DbSet<ArchetypeSync> ArchetypeModels { get; set; }

    }
}
