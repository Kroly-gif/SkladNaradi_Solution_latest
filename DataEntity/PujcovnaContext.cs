using DataEntity.Data;
using Microsoft.EntityFrameworkCore;

namespace DataEntity
{
    public class PujcovnaContext : DbContext
    {
        public DbSet<Naradi> Naradi { get; set; }
        public DbSet<Zakaznik> Zakaznici { get; set; }
        public DbSet<Vypujcka> Vypujcky { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PujcovnaNaradiDB;Trusted_Connection=True;");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}