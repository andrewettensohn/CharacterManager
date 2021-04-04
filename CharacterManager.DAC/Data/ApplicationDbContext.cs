using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data
{
    public class ApplicationDbContext : DbContext
    {
        private IConfiguration _configuration { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
            { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqliteConnection connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlite(connection);
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
        public DbSet<ArchetypeLink> ArchetypeLink { get; set; }
        public DbSet<TalentLink> TalentLink { get; set; }
        public DbSet<GearLink> WargearLink { get; set; }
        public DbSet<ArmorLink> ArmorLink { get; set; }
        public DbSet<WeaponLink> WeaponLink { get; set; }
        public DbSet<GearLink> GearLink { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ConfigParam> ConfigParams { get; set; }
    }
}
