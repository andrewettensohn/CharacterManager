﻿using CharacterManager.DAC.Models;
using CharacterManager.Models;
using CharacterManager.Models.Links;
using CharacterManager.Sync.API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CharacterManager.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"characterLocal.db" };
            string connectionString = connectionStringBuilder.ToString();
            SqliteConnection connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public DbSet<SyncModel> SyncModels { get; set; }
        //public DbSet<CharacterSync> CharacterSync { get; set; }
        //public DbSet<Attributes> Attributes { get; set; }
        //public DbSet<Skills> Skills { get; set; }
        //public DbSet<Talent> Talent { get; set; }
        //public DbSet<PyschicPower> PsychicPowers { get; set; }
        //public DbSet<Weapon> Wargear { get; set; }
        //public DbSet<Archetype> Archetype { get; set; }
        //public DbSet<Gear> Gear { get; set; }
        //public DbSet<Weapon> Weapon { get; set; }
        //public DbSet<Armor> Armor { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<SyncStatus> SyncStatus { get; set; }
        //public DbSet<QuestSync> QuestSync { get; set; }

    }
}
