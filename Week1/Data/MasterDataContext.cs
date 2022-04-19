using Microsoft.EntityFrameworkCore;
using Week1.Models;
namespace Week1.Data
{
    public class MasterDataContext : DbContext
    {
        public MasterDataContext(DbContextOptions<MasterDataContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<Item>().HasKey(x => x.Id);
            modelBuilder.Entity<Item>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
