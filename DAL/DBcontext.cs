using Microsoft.EntityFrameworkCore;
using Models;
namespace DAL
{
    public class DBcontext : DbContext
    {// Tabeller
        public DbSet<Användare> Användare { get; set; }
        public DbSet<Kund> Kunder { get; set; }
        public DbSet<Produkt> Produkter { get; set; }
        public DbSet<LagerfördProdukt> LagerfördaProdukter { get; set; }
        public DbSet<SpecialBeställning> SpecialBeställningar { get; set; }
        public DbSet<Order> Ordrar { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialBeställning> MaterialBeställningar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ansluter till LocalDB
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HattmakarenDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. ARVSHANTERING (TPH - Table Per Hierarchy)
            // Detta gör att LagerfördProdukt och SpecialBeställning sparas i samma tabell ("Produkter")
            // men skiljs åt av en "ProduktTyp"-kolumn.
            modelBuilder.Entity<Produkt>()
                .HasDiscriminator<string>("ProduktTyp")
                .HasValue<LagerfördProdukt>("Lagerförd")
                .HasValue<SpecialBeställning>("Special");

            // 2. RELATION: Kund -> Order (1:N)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Kund)
                .WithMany(k => k.Orders)
                .HasForeignKey(o => o.KundID)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. RELATION: Order -> Produkt (1:N)
            // Här använder vi den nya gemensamma listan i Order
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Produkter)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderID)
                .OnDelete(DeleteBehavior.Cascade); // Om ordern tas bort, försvinner produkterna i den

            // 4. RELATIONER TILL ANVÄNDARE (Era nya ändringar)

            // Användare -> Order (Vem som startade ordern)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.StartadAv)
                .WithMany(u => u.orderLista)
                .HasForeignKey(o => o.StartadAvID)
                .OnDelete(DeleteBehavior.Restrict);

            // Användare -> Produkt (Vem som tillverkade hatten)
            modelBuilder.Entity<Produkt>()
                .HasOne(p => p.TillverkadAv)
                .WithMany(u => u.produktLista)
                .HasForeignKey(p => p.TillverkadAVID)
                .OnDelete(DeleteBehavior.Restrict);

            // Användare -> MaterialBeställning (Vem som gjorde beställningen)
            modelBuilder.Entity<MaterialBeställning>()
                .HasOne(mb => mb.StartadAv)
                .WithMany(u => u.materialBeställningsLista)
                .HasForeignKey(mb => mb.StartadAvID)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. MÅNGA-TILL-MÅNGA (N:N)
            // Produkt <-> Material
            modelBuilder.Entity<Produkt>()
                .HasMany(p => p.MaterialLista)
                .WithMany();

            // MaterialBeställning <-> Material
            modelBuilder.Entity<MaterialBeställning>()
                .HasMany(mb => mb.MaterialLista)
                .WithMany();

            // 6. DATATYPSPRECISION (Ekonomi)
            // Tvingar SQL Server att använda decimaler för priser för att undvika avrundningsfel.
            modelBuilder.Entity<Produkt>().Property(p => p.pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Material>().Property(m => m.Pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MaterialBeställning>().Property(mb => mb.TotalPris).HasColumnType("decimal(18,2)");

        }

    }
}
