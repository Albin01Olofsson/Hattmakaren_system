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
        public DbSet<Planering> Planeringar { get; set; }
        public DbSet<Aktivitet> Aktiviteter { get; set; }
        public DbSet<BestallningsRad> BestallningsRader { get; set; }
        public DbSet<OrderRad> OrderRader { get; set; }
        public DbSet<Reklamation> Reklamationer { get; set; }
        public DbSet<ProduktMaterial> ProduktMaterial { get; set; }

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

            // 1. Konfigurera En-till-många (Skaparen)
            modelBuilder.Entity<Aktivitet>()
                .HasOne(ak => ak.SkapadAv)
                .WithMany(u => u.SkapadeAktiviteter)
                .HasForeignKey(ak => ak.SkapadAvID)
                .OnDelete(DeleteBehavior.Restrict); // Viktigt: Vi vill inte radera användaren om aktiviteten tas bort

            // 2. Konfigurera Många-till-många (Deltagarna)
            modelBuilder.Entity<Aktivitet>()
                .HasMany(ak => ak.Deltagare)
                .WithMany(u => u.DeltarIAktiviteter)
                .UsingEntity(j => j.ToTable("AnvändarAktiviteter"));
            // 4. RELATIONER TILL ANVÄNDARE (Era nya ändringar)

            // Användare -> Order (Vem som startade ordern)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.StartadAv)
                .WithMany(u => u.orderLista)
                .HasForeignKey(o => o.StartadAvID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(o => o.Rabatt)
                .HasPrecision(5, 2);

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
            //modelBuilder.Entity<Produkt>()
            //    .HasMany(p => p.MaterialLista)
            //    .WithMany();
            modelBuilder.Entity<ProduktMaterial>()
                .HasKey(pm => pm.ProduktMaterialID);

            modelBuilder.Entity<ProduktMaterial>()
                .HasOne(pm => pm.Produkt)
                .WithMany(p => p.ProduktMaterial)
                .HasForeignKey(pm => pm.ProduktID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProduktMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany(m => m.ProduktMaterial)
                .HasForeignKey(pm => pm.MaterialID)
                .OnDelete(DeleteBehavior.Restrict);



            // MaterialBeställning <-> Material
            modelBuilder.Entity<MaterialBeställning>()
                .HasMany(mb => mb.MaterialLista)
                .WithMany();


            modelBuilder.Entity<Planering>(entity =>
            {
                // Primärnyckel
                entity.HasKey(p => p.PlaneringsID);

                // 1 -> många (Användare -> Planeringar)
                entity.HasOne(p => p.Användare)
                      .WithMany(a => a.Planeringar)
                      .HasForeignKey(p => p.AnvändarID)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.OrderRad)
                    .WithMany(or => or.Planeringar)
                    .HasForeignKey(p => p.OrderRadID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderRader)
                .WithOne(or => or.Order)
                .HasForeignKey(or => or.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Produkt>()
                   .HasMany(p => p.OrderRader)
                   .WithOne(or => or.Produkt)
                   .HasForeignKey(or => or.ProduktID)
                   .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderRad>(entity =>
            {
                entity.HasKey(or => or.OrderRadID);

                entity.Property(or => or.Antal)
                      .IsRequired();
            });

            modelBuilder.Entity<Reklamation>(entity =>
            {
                entity.HasOne(r => r.Order)
                      .WithMany()
                      .HasForeignKey(r => r.OrderID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Kund)
                      .WithMany()
                      .HasForeignKey(r => r.KundID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Produkt)
                      .WithMany()
                      .HasForeignKey(r => r.ProduktID)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(r => r.SkapadAv)
                      .WithMany()
                      .HasForeignKey(r => r.SkapadAvID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 6. DATATYPSPRECISION (Ekonomi)
            // Tvingar SQL Server att använda decimaler för priser för att undvika avrundningsfel.
            modelBuilder.Entity<Produkt>().Property(p => p.Pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Material>().Property(m => m.Pris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MaterialBeställning>().Property(mb => mb.TotalPris).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ProduktMaterial>().Property(mb => mb.Mängd).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(o => o.Rabatt).HasColumnType("decimal(18,2)");


            //Exempeldata ----------------

            //ANVÄNDARE
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Hattkungen1");
            modelBuilder.Entity<Användare>().HasData(
                    new Användare
                    {
                        AnvändarID = 1,
                        Namn = "Otto",
                        Telefon = "07085652321",
                        Email = "ottoHattman@hotmail.com",
                        Lösenord = passwordHash,
                        IsAdmin = true
                    },
                    new Användare
                    {
                        AnvändarID = 2,
                        Namn = "Judith",
                        Telefon = "0727639856",
                        Email = "JudithHattman@hotmail.com",
                        Lösenord = passwordHash,
                        IsAdmin = false
                    },
                    new Användare
                    {
                        AnvändarID = 3,
                        Namn = "Millie",
                        Telefon = "0709825533",
                        Email = "MillieHattman@hotmail.com",
                        Lösenord = passwordHash,
                        IsAdmin = false
                    },
                    new Användare
                    {
                        AnvändarID = 4,
                        Namn = "Herbert",
                        Telefon = "0705512322",
                        Email = "HerbertHattman@hotmail.com",
                        Lösenord = passwordHash,
                        IsAdmin = false
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
                        Email = "Per.Larsson@hotmail.com",
                        FöretagsKund = true,
                        Land = "Sverige",
                        Stad = "Stockholm"
                    },
                    new Kund
                    {
                        KundID = 1002,
                        Namn = "Eva Von Milen",
                        Adress = "Milvägen 1",
                        Telefon = "0727728432",
                        Email = "Eva.Milen@hotmail.com",
                        FöretagsKund = true,
                        Land = "Sverige",
                        Stad = "Stockholm"
                    },
                    new Kund
                    {
                        KundID = 1003,
                        Namn = "Yvonne Fjord",
                        Adress = "Fjordaberg 51",
                        Telefon = "0702127345",
                        Email = "yvonne.fjord@hotmail.com",
                        FöretagsKund = false,
                        Land = "Finland",
                        Stad = "Helsingfors"
                    },
                    new Kund
                    {
                        KundID = 1004,
                        Namn = "Ahmed Khan",
                        Adress = "Javatorget 23",
                        Telefon = "070123382",
                        Email = "ahmed.khan@hotmail.com",
                        FöretagsKund = false,
                        Land = "Sverige",
                        Stad = "Örebro"
                    },
                    new Kund
                    {
                        KundID = 1005,
                        Namn = "Jasmin Barsk",
                        Adress = "Tetornet 3",
                        Telefon = "0702427373",
                        Email = "jasmin.barsk@hotmail.com",
                        FöretagsKund = false,
                        Land = "Sverige",
                        Stad = "Stockholm"
                    }
                );
            //MATERIAL
            modelBuilder.Entity<Material>().HasData(
                    new Material
                    {
                        MaterialID = 100001,
                        Namn = "Filt",
                        Pris = 54,
                        Beskrivning = "Inte filt man sover med",
                        MåttTyp = MåttTyp.Meter,
                        Lagerantal = 23
                    },
                    new Material
                    {
                        MaterialID = 100002,
                        Namn = "Bomull",
                        Pris = 34,
                        Beskrivning = "100% obesprutat bomull",
                        MåttTyp = MåttTyp.Meter,
                        Lagerantal = 52
                    },
                    new Material
                    {
                        MaterialID = 100003,
                        Namn = "Svart tråd",
                        Pris = 28,
                        Beskrivning = "1.2 mm svar syträd av silikon och polyester",
                        MåttTyp = MåttTyp.Meter,
                        Lagerantal = 2
                    },
                    new Material
                    {
                        MaterialID = 100004,
                        Namn = "Siden",
                        Pris = 89,
                        Beskrivning = "Tunt siden till foder och detaljer",
                        MåttTyp = MåttTyp.Meter,
                        Lagerantal = 18
                    },
                    new Material
                    {
                        MaterialID = 100005,
                        Namn = "Läderband",
                        Pris = 45,
                        Beskrivning = "Brunt läderband till hattdekoration",
                        MåttTyp = MåttTyp.Meter,
                        Lagerantal = 11
                    }
                );
            //MATERIALBESTÄLLNINGAR
            modelBuilder.Entity<MaterialBeställning>().HasData(
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000001,
                        TotalPris = 1890,
                        StartadAvID = 1,
                        Leverantör = "Kung AB",
                        Datum = new DateTime(2026, 1, 15)
                    },
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000002,
                        TotalPris = 769,
                        StartadAvID = 2,
                        Leverantör = "Nordic Textile",
                        Datum = new DateTime(2026, 2, 12)
                    },
                    new MaterialBeställning
                    {
                        MaterialBeställningID = 1000003,
                        TotalPris = 3419,
                        StartadAvID = 1,
                        Leverantör = "Skrädderi Grossisten",
                        Datum = new DateTime(2026, 3, 5)
                    }
                );
            modelBuilder.Entity<BestallningsRad>().HasData(
                    new BestallningsRad { Id = 10001, MaterialBeställningID = 1000001, MaterialId = 100001, Antal = 20 },
                    new BestallningsRad { Id = 10002, MaterialBeställningID = 1000001, MaterialId = 100003, Antal = 10 },
                    new BestallningsRad { Id = 10003, MaterialBeställningID = 1000002, MaterialId = 100002, Antal = 30 },
                    new BestallningsRad { Id = 10004, MaterialBeställningID = 1000002, MaterialId = 100004, Antal = 12 },
                    new BestallningsRad { Id = 10005, MaterialBeställningID = 1000003, MaterialId = 100001, Antal = 35 },
                    new BestallningsRad { Id = 10006, MaterialBeställningID = 1000003, MaterialId = 100005, Antal = 15 }
                );
            //MATERIALMATERIALPRODUKT
            //är en mellantabell, kodade in relationerna vi keyes istället bara

            //MATERIALPRODUKT

            //ORDRAR
            modelBuilder.Entity<Order>().HasData(
                    new Order
                    {
                        OrderID = 100000001,
                        Pris = 1299,
                        Rabatt = 0,
                        Datum = new DateTime(2024, 6, 11),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 1,
                        KundID = 1001,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000002,
                        Pris = 1099,
                        Rabatt = 0,
                        Datum = new DateTime(2024, 8, 1),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 1,
                        KundID = 1002,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000003,
                        Pris = 299,
                        Rabatt = 0,
                        Datum = new DateTime(2024, 6, 21),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 1,
                        KundID = 1003,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000004,
                        Pris = 2399,
                        Rabatt = 0,
                        Datum = new DateTime(2024, 6, 21),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 1,
                        KundID = 1004,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000005,
                        Pris = 779,
                        Rabatt = 0,
                        Datum = new DateTime(2024, 6, 21),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 1,
                        KundID = 1005,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000006,
                        Pris = 949,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 2, 18),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 2,
                        KundID = 1001,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000007,
                        Pris = 1049,
                        Rabatt = 0,
                        Datum = new DateTime(2025, 10, 6),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 2,
                        KundID = 1002,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000008,
                        Pris = 749,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 2,
                        KundID = 1003,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000009,
                        Pris = 999,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 2,
                        KundID = 1004,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000010,
                        Pris = 899,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 2,
                        KundID = 1004,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000011,
                        Pris = 1099,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 2,
                        KundID = 1005,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000012,
                        Pris = 2019,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 3,
                        KundID = 1001,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000013,
                        Pris = 1829,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 3,
                        KundID = 1002,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000014,
                        Pris = 599,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 3,
                        KundID = 1003,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000015,
                        Pris = 899,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 3,
                        KundID = 1004,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000016,
                        Pris = 1299,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = true,
                        StartadAvID = 3,
                        KundID = 1005,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000017,
                        Pris = 499,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 4,
                        KundID = 1001,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000018,
                        Pris = 499,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 4,
                        KundID = 1002,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000019,
                        Pris = 499,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 4,
                        KundID = 1003,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000020,
                        Pris = 499,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 4,
                        KundID = 1004,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    },
                    new Order
                    {
                        OrderID = 100000021,
                        Pris = 499,
                        Rabatt = 0,
                        Datum = new DateTime(2026, 4, 11),
                        Färdig = false,
                        IsSpecialbeställning = false,
                        StartadAvID = 4,
                        KundID = 1005,
                        Status = "Ej påbörjat",
                        FörväntadTillverkningsTid = new DateTime(2026, 04, 28)
                    }
                );
            //PRODUKTER
            modelBuilder.Entity<LagerfördProdukt>().HasData(
                    new LagerfördProdukt
                    {
                        ProduktID = 10000001,
                        Namn = "Filt hatt",
                        Pris = 1099,
                        Storlek = "M",
                        TillverkadAVID = 1,
                        ArtikelID = "LP0001",
                        Kategori = "Hatt",
                        HattTyp = "Fedora",
                        Modell = "Klassisk",
                        Färg = "Svart",
                        Decoration = "Läderband",
                        Lagerantal = 7,
                        Färdig = true
                    },
                    new LagerfördProdukt
                    {
                        ProduktID = 10000002,
                        Namn = "Siden keps",
                        Pris = 949,
                        Storlek = "M",
                        TillverkadAVID = 2,
                        ArtikelID = "LP0002",
                        Kategori = "Keps",
                        HattTyp = "Keps",
                        Modell = "Siden",
                        Färg = "Blå",
                        Decoration = "Svart tråd",
                        Lagerantal = 5,
                        Färdig = true
                    },
                    new LagerfördProdukt
                    {
                        ProduktID = 10000003,
                        Namn = "Sommarhatt",
                        Pris = 799,
                        Storlek = "L",
                        TillverkadAVID = 3,
                        ArtikelID = "LP0003",
                        Kategori = "Hatt",
                        HattTyp = "Panama",
                        Modell = "Sommar",
                        Färg = "Naturvit",
                        Decoration = "Bomullsband",
                        Lagerantal = 4,
                        Färdig = true
                    }
                );
            modelBuilder.Entity<SpecialBeställning>().HasData(
                    new SpecialBeställning
                    {
                        ProduktID = 10000004,
                        Namn = "Bröllopshatt",
                        Pris = 1899,
                        Storlek = "S",
                        TillverkadAVID = 4,
                        HattTyp = "Fascinator",
                        Modell = "Bröllop",
                        Färg = "Creme",
                        Decoration = "Sidenrosett",
                        Lagerantal = 0,
                        Färdig = false,
                        BildURL = "",
                        Beskrivning = "Specialbeställd bröllopshatt med sidenrosett"
                    }
                );
            modelBuilder.Entity<OrderRad>().HasData(
                    new OrderRad { OrderRadID = 20001, OrderID = 100000001, ProduktID = 10000004, Antal = 1 },
                    new OrderRad { OrderRadID = 20002, OrderID = 100000002, ProduktID = 10000001, Antal = 1 },
                    new OrderRad { OrderRadID = 20003, OrderID = 100000003, ProduktID = 10000003, Antal = 1 },
                    new OrderRad { OrderRadID = 20004, OrderID = 100000004, ProduktID = 10000004, Antal = 1 },
                    new OrderRad { OrderRadID = 20005, OrderID = 100000005, ProduktID = 10000002, Antal = 1 },
                    new OrderRad { OrderRadID = 20006, OrderID = 100000006, ProduktID = 10000002, Antal = 1 },
                    new OrderRad { OrderRadID = 20007, OrderID = 100000007, ProduktID = 10000001, Antal = 1 },
                    new OrderRad { OrderRadID = 20008, OrderID = 100000008, ProduktID = 10000003, Antal = 2 },
                    new OrderRad { OrderRadID = 20009, OrderID = 100000009, ProduktID = 10000001, Antal = 1 },
                    new OrderRad { OrderRadID = 20010, OrderID = 100000010, ProduktID = 10000002, Antal = 1 },
                    new OrderRad { OrderRadID = 20011, OrderID = 100000011, ProduktID = 10000001, Antal = 1 },
                    new OrderRad { OrderRadID = 20012, OrderID = 100000012, ProduktID = 10000004, Antal = 1 },
                    new OrderRad { OrderRadID = 20013, OrderID = 100000013, ProduktID = 10000004, Antal = 1 },
                    new OrderRad { OrderRadID = 20014, OrderID = 100000014, ProduktID = 10000003, Antal = 1 },
                    new OrderRad { OrderRadID = 20015, OrderID = 100000015, ProduktID = 10000002, Antal = 1 },
                    new OrderRad { OrderRadID = 20016, OrderID = 100000016, ProduktID = 10000004, Antal = 1 },
                    new OrderRad { OrderRadID = 20017, OrderID = 100000017, ProduktID = 10000003, Antal = 1 },
                    new OrderRad { OrderRadID = 20018, OrderID = 100000018, ProduktID = 10000001, Antal = 1 },
                    new OrderRad { OrderRadID = 20019, OrderID = 100000019, ProduktID = 10000002, Antal = 1 },
                    new OrderRad { OrderRadID = 20020, OrderID = 100000020, ProduktID = 10000003, Antal = 1 },
                    new OrderRad { OrderRadID = 20021, OrderID = 100000021, ProduktID = 10000001, Antal = 1 }
                );
            modelBuilder.Entity<Reklamation>().HasData(
                    new Reklamation
                    {
                        ReklamationID = 30001,
                        OrderID = 100000008,
                        KundID = 1003,
                        ProduktID = 10000003,
                        Orsak = "Fel storlek",
                        Beskrivning = "Kunden önskar justering av passform.",
                        Status = "Ny",
                        Atgard = "Justering",
                        SkapadDatum = new DateTime(2026, 4, 18),
                        SkapadAvID = 1
                    },
                    new Reklamation
                    {
                        ReklamationID = 30002,
                        OrderID = 100000013,
                        KundID = 1002,
                        ProduktID = 10000004,
                        Orsak = "Fel färg",
                        Beskrivning = "Sidenrosetten behöver bytas till ljusare nyans.",
                        Status = "Under behandling",
                        Atgard = "Reparation",
                        SkapadDatum = new DateTime(2026, 4, 20),
                        SkapadAvID = 2
                    }
                );
            //modelBuilder.Entity("MaterialProdukt").HasData(
            //        new { MaterialListaMaterialID = 100001, ProduktID = 10000001 },
            //        new { MaterialListaMaterialID = 100003, ProduktID = 10000001 },
            //        new { MaterialListaMaterialID = 100005, ProduktID = 10000001 },
            //        new { MaterialListaMaterialID = 100004, ProduktID = 10000002 },
            //        new { MaterialListaMaterialID = 100003, ProduktID = 10000002 },
            //        new { MaterialListaMaterialID = 100002, ProduktID = 10000003 },
            //        new { MaterialListaMaterialID = 100005, ProduktID = 10000003 },
            //        new { MaterialListaMaterialID = 100004, ProduktID = 10000004 },
            //        new { MaterialListaMaterialID = 100002, ProduktID = 10000004 },
            //        new { MaterialListaMaterialID = 100003, ProduktID = 10000004 }
            //    );
            modelBuilder.Entity("MaterialMaterialBeställning").HasData(
                    new { MaterialBeställningID = 1000001, MaterialListaMaterialID = 100001 },
                    new { MaterialBeställningID = 1000001, MaterialListaMaterialID = 100003 },
                    new { MaterialBeställningID = 1000002, MaterialListaMaterialID = 100002 },
                    new { MaterialBeställningID = 1000002, MaterialListaMaterialID = 100004 },
                    new { MaterialBeställningID = 1000003, MaterialListaMaterialID = 100001 },
                    new { MaterialBeställningID = 1000003, MaterialListaMaterialID = 100005 }
                );
        }

    }
}
