using DataEntity.Base;
using DataEntity.Data;
using Microsoft.EntityFrameworkCore;

namespace DataEntity
{
    public class PujcovnaContext : DbContext
    {
        // Tabulky
        public DbSet<Zakaznik> Zakaznici { get; set; }
        public DbSet<Naradi> Naradi { get; set; }
        public DbSet<Vypujcka> Vypujcky { get; set; }

        // Prázdný konstruktor
        public PujcovnaContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Standardní connection string pro školní LocalDB
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PujcovnaNaradiDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
    }
}