using Microsoft.EntityFrameworkCore;
using GeneratedWorlds.Infrastructure.DataModels;
using GeneratedWorlds.Infrastructure.DataModels.Items;
using GeneratedWorlds.Domain.Types;

namespace GeneratedWorlds.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<CharacterDataModel> Characters { get; set; }
        public DbSet<CharacterSkillDataModel> CharacterSkills { get; set; }

        // This is key: use the base class for TPH
        public DbSet<ItemDataModel> Items { get; set; }

        public DbSet<CharacterInventoryItemDataModel> InventoryItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enable TPT: base class → its table, derived classes → their own tables
            modelBuilder.Entity<ItemDataModel>()
                .ToTable("Items"); // optional base table

            modelBuilder.Entity<PotionDataModel>()
                .ToTable("Potions"); // creates Potions table

            // Character relationships
            modelBuilder.Entity<CharacterDataModel>()
                .HasMany(c => c.Inventory)
                .WithOne(i => i.Character)
                .HasForeignKey(i => i.CharacterReference)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterSkillDataModel>()
                .HasOne(s => s.Character)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CharacterReference);
        }

    }
}
