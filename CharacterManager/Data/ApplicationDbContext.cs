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

        public DbSet<Character> Character { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<MentalTraits> MentalTraits { get; set; }
        public DbSet<SocialTraits> SocialTraits { get; set; }
        public DbSet<CombatTraits> CombatTraits { get; set; }
        public DbSet<Talent> Talent { get; set; }
        public DbSet<Wargear> Wargear { get; set; }
        public DbSet<Archetype> Archetype { get; set; }
        public DbSet<ArchetypeAbility> ArchetypeAbility { get; set; }
        public DbSet<TalentLink> TalentLink { get; set; }
        public DbSet<WargearLink> WargearLink { get; set; }

    }
}
