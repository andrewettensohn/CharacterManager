using CharacterManager.DAC.Models;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterManager.Sync.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

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
        public DbSet<SyncStatus> SyncStatus { get; set; }

    }
}
