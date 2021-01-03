using CharacterManager.Models;
using CharacterManager.Models.CharacterLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Character> Characters { get; set; }

        public DbSet<CharacterAction> CharacterActions { get; set; }

        public DbSet<CharacterClass> CharacterClasses { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<Spell> Spells { get; set; }

        public DbSet<Stat> Stats { get; set; }

        public DbSet<CharacterActionLink> CharacterActionLink { get; set; }

        public DbSet<CharacterClassLink> CharacterClassLink { get; set; }

        public DbSet<EquipmentLink> EquipmentLink { get; set; }

        public DbSet<FeatureLink> FeatureLink { get; set; }

        public DbSet<RaceLink> RaceLink { get; set; }

        public DbSet<SpellLink> SpellLink { get; set; }

        public DbSet<StatLink> StatLink { get; set; }
    }
}
