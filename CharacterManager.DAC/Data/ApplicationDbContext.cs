using CharacterManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Sync.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GearLink>()
            //    .HasKey(c => new { c.CharacterId, c.GearId });
        }

        public DbSet<Character> Character { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Talent> Talent { get; set; }
        public DbSet<Weapon> Wargear { get; set; }
        public DbSet<Archetype> Archetype { get; set; }
        public DbSet<Gear> Gear { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
        public DbSet<Armor> Armor { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ConfigParam> ConfigParams { get; set; }
    }
}
