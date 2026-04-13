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

            //Exempeldata ----------------

            //ANVÄNDARE
            modelBuilder.Entity<Användare>().HasData(
                    new Användare { 
                        AnvändarID = 1,
                        Namn = "Otto",
                        Telefon = "07085652321",
                        Email = "ottoHattman@hotmail.com",
                        Lösenord = "Hattkungen1"
                    },
                    new Användare { 
                        AnvändarID = 2,
                        Namn = "Judith",
                        Telefon = "0727639856",
                        Email = "JudithHattman@hotmail.com",
                        Lösenord = "HattPrinsessan1"
                    }
                );

            //KUNDER
            modelBuilder.Entity<Kund>().HasData(
                    new Kund
                    {
                        KundID = 1001,
                        Namn = "Per Larsson",
                        Adress = "Kullstigen 78",
                        Telefon = "076312129",
                        Email = "Per.Larsson@hotmail.com"
                    },
                    new Kund
                    {
                        KundID = 1002,
                        Namn = "Eva Von Milen",
                        Adress = "Milvägen 1",
                        Telefon = "0727728432",
                        Email = "Eva.Milen@hotmail.com"
                    },
                    new Kund
                    {
                        KundID = 1003,
                        Namn = "Yvonne Fjord",
                        Adress = "Fjordaberg 51",
                        Telefon = "0702127345",
                        Email = "yvonne.fjord@hotmail.com"
                    }
                );
            //MATERIAL
            modelBuilder.Entity<Material>().HasData(
                    new Material { 
                        MaterialID = 100001,
                        Namn = "Filt",
                        Pris = 54,
                        Beskrivning = "Inte filt man sover med",
                        Typ = "Tyg",
                        Lagerantal = 23
                    },
                    new Material { 
                        MaterialID = 100002,
                        Namn = "Bomull",
                        Pris = 34,
                        Beskrivning = "100% obesprutat bomull",
                        Typ = "Tyg",
                        Lagerantal = 52
                    },
                    new Material { 
                        MaterialID = 100003,
                        Namn = "Svart tråd",
                        Pris = 28,
                        Beskrivning = "1.2 mm svar syträd av silikon och polyester",
                        Typ = "Tråd",
                        Lagerantal = 2
                    }
                );
            //MATERIALBESTÄLLNINGAR
            modelBuilder.Entity<MaterialBeställning>().HasData(
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000001,
                        TotalPris = 1890,
                        StartadAvID = 1
                    },
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000002,
                        TotalPris = 769,
                        StartadAvID = 2
                    },
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000003,
                        TotalPris = 3419,
                        StartadAvID = 1
                    }
                );
            //MATERIALMATERIALPRODUKT
            //är en mellantabell, kodade in relationerna vi keyes istället bara

            //MATERIALPRODUKT

            //ORDRAR
            modelBuilder.Entity<Order>().HasData(
                    new Order { 
                        OrderID = 100000001,
                        Pris = 1099,
                        Datum = new DateTime(2024, 6, 11),
                        Färdig = true,
                        StartadAvID = 1,
                        KundID = 1001
                    },
                    new Order { 
                        OrderID = 100000002,
                        Pris = 949,
                        Datum = new DateTime(2025, 1, 18),
                        Färdig = true,
                        StartadAvID = 2,
                        KundID = 1002
                    },
                    new Order { 
                        OrderID = 100000003,
                        Pris = 1899,
                        Datum = new DateTime(2026, 3, 21),
                        Färdig = false,
                        StartadAvID = 1,
                        KundID = 1001
                    }
                );
            //PRODUKTER
            modelBuilder.Entity<Produkt>().HasData(
                    new Produkt
                    {
                        ProduktID = 10000001,
                        namn = "Filt hatt",
                        pris = 1099,
                        Storlek = "M",
                        OrderID = 100000001,
                        TillverkadAVID = 1
                    },
                    new Produkt
                    {
                        ProduktID = 10000002,
                        namn = "Siden hatt",
                        pris = 949,
                        Storlek = "M",
                        OrderID = 100000002,
                        TillverkadAVID = 2
                    }
                );
        }

    }
}
