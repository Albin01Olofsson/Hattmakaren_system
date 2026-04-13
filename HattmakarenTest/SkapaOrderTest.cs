using BL.Services;
using DAL;
using DAL.Repositorys;
using Models;

namespace Hattmakarna.Tests
{
    public class SkapaOrderTest
    {
        [Fact]
        public void SkapaOrder_SkaSparaOrderMedKoppladData()
        {
            // --- 1. ARRANGE ---
            using var context = new DBcontext();

            // Vi säkerställer att databasen är skapad och att din Seed-data (Otto, Per etc.) finns där.
            context.Database.EnsureCreated();

            var repo = new OrderRepo(context);
            var service = new OrderService(repo);

            // Vi skapar en ny hatt för denna specifika order
            var nyHatt = new Produkt
            {
                namn = "Test-Cylinder",
                pris = 1200,
                Storlek = "L",
                TillverkadAVID = 1 // Otto
            };

            var nyOrder = new Order
            {
                // Vi använder ID:n från din testdata ovan
                KundID = 1001,       // Per Larsson
                StartadAvID = 2,     // Judith
                Produkter = new List<Produkt> { nyHatt },
                Pris = 1200,
                Färdig = false
            };

            // --- 2. ACT ---
            service.skapaOrder(nyOrder);

            // --- 3. ASSERT ---
            // Vi hämtar ordern med detaljer för att se att allt hänger ihop
            var sparadOrder = service.HämtaMedDetaljer(nyOrder.OrderID);

            Assert.NotNull(sparadOrder);
            Assert.Equal("Per Larsson", sparadOrder.Kund.Namn); // Verifierar kopplingen till Seed-data
            Assert.Equal("Judith", sparadOrder.StartadAv.Namn); // Verifierar inloggad användare
            Assert.Single(sparadOrder.Produkter);
            Assert.Equal("Test-Cylinder", sparadOrder.Produkter[0].namn);

            // Kontrollera att datumet sattes i servicen
            Assert.Equal(DateTime.Now.Date, sparadOrder.Datum.Date);
        }

        [Fact]
        public void SkapaOrder_UtanProdukter_SkaKastaException()
        {
            // ARRANGE
            using var context = new DBcontext();
            var repo = new OrderRepo(context);
            var service = new OrderService(repo);

            var felaktigOrder = new Order
            {
                KundID = 1001,
                StartadAvID = 1,
                Produkter = new List<Produkt>() // Tom lista ska inte vara tillåtet enligt din service
            };

            // ACT & ASSERT
            var ex = Assert.Throws<ArgumentException>(() => service.skapaOrder(felaktigOrder));
            Assert.Contains("minst en produkt", ex.Message);
        }
    }
}