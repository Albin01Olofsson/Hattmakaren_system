using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL;
using DAL.Intefaces;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class FraktjaktService : IFraktjaktService
    {
        private readonly DBcontext _dbcontext;
        private readonly IOrderRepository _orderRepo;

        public FraktjaktService(IOrderRepository orderRepo, DBcontext dBcontext)
        {
            _orderRepo = orderRepo;
            _dbcontext = dBcontext;
        }
        public async Task<List<SpårningsPunkt>> HämtaHistorik(string sändningsnummer)
        {
            //simulering fördröjning
            await Task.Delay(400);

            if(sändningsnummer.StartsWith("TEST-"))                             //TEST-rutt
            {
                return await GenereraTestRutt(sändningsnummer);
            }

            var frakt = await _orderRepo.GetFraktBySändningsnummer(sändningsnummer);
            var kund = frakt.Order.Kund;

            DateTime simuleratStart;
            switch(sändningsnummer)
            {
                case "S-100004": simuleratStart = DateTime.Now.AddDays(-10); break;
                case "S-100003": simuleratStart = DateTime.Now.AddDays(-5); break;
                case "S-100001": simuleratStart = DateTime.Now.AddDays(-3); break;
                default: simuleratStart = frakt.StartDatum; break; 
            }

            if (frakt == null) return new List<SpårningsPunkt>();

            TimeSpan tidSedanStart = DateTime.Now - simuleratStart;

            var ruttMall = new List<(TimeSpan AktiverasEfter, SpårningsPunkt Punkt)>
            {
                (TimeSpan.FromMinutes(0), new SpårningsPunkt { Plats = "Lager (Butik)", Meddelande = "Order plockad och packad", Latitud = 59.2662, Longitud = 15.2104 }),
                (TimeSpan.FromMinutes(10), new SpårningsPunkt { Plats = "Örebro Terminal", Meddelande = "Inlämnad till transportör", Latitud = 59.2753, Longitud = 15.2134 }),
                (TimeSpan.FromHours(1), new SpårningsPunkt { Plats = "Västerås Hub", Meddelande = "Sorterad och skickad vidare", Latitud = 59.6099, Longitud = 16.5448 }),
                (TimeSpan.FromHours(4), new SpårningsPunkt { Plats = "Stockholm", Meddelande = "Ankommit sorteringsterminal", Latitud = 59.3293, Longitud = 18.0686 }),
                (TimeSpan.FromHours(8), new SpårningsPunkt { Plats = kund.Stad, Meddelande = "Lastad på bil för utkörning", Latitud = 59.3689, Longitud = 18.0084 })
            };

            var synligaPunkter = ruttMall                                                   //För att varje punkt ska komma i "realtid"
                .Where(x => tidSedanStart >= x.AktiverasEfter)
                .Select(x => {
                    x.Punkt.Tidpunkt = simuleratStart.Add(x.AktiverasEfter);
                    return x.Punkt;
                })
                .OrderByDescending(p => p.Tidpunkt)
                .ToList();

            return synligaPunkter;
        }

        public async Task<List<SpårningsPunkt>> GenereraTestRutt(string sändningsnummer)
        {
            var frakt = await _dbcontext.Frakt.Include(f => f.Order).ThenInclude(o => o.Kund).FirstOrDefaultAsync(f => f.Sändningsnummer == sändningsnummer);

            if (frakt == null) return new List<SpårningsPunkt>();

            var kund = frakt.Order.Kund;
            var start = frakt.StartDatum;

            return kund.Stad switch
            {
                "Stockholm" => new List<SpårningsPunkt>
                {
                    new SpårningsPunkt {Tidpunkt = start.AddDays(1).AddHours(6), Plats = kund.Stad, Meddelande = "Ankommit destination", Latitud = HämtaLat(kund.Stad), Longitud = HämtaLng(kund.Stad)},
                    new SpårningsPunkt {Tidpunkt = start.AddHours(18), Plats = "Västerås", Meddelande = "Passerat kontrollstation", Latitud = 57.1837, Longitud = 14.0463 },
                    new SpårningsPunkt {Tidpunkt = start.AddHours(5), Plats = "Örebro", Meddelande = "Sorterad", Latitud = 59.2753, Longitud = 15.2134 }
                },
                "Helsingfors" => new List<SpårningsPunkt>
                {
                    new SpårningsPunkt {Tidpunkt = start.AddDays(1).AddHours(8), Plats = $"{kund.Stad} Logistikcenter", Meddelande = "Lastas på bil", Latitud = HämtaLat(kund.Stad), Longitud = HämtaLng(kund.Stad) },
                    new SpårningsPunkt {Tidpunkt = start.AddHours(12), Plats = "Kapellskär (Färja)", Meddelande = "Lastad på fartyg", Latitud = 59.7208, Longitud = 19.0633},
                    new SpårningsPunkt {Tidpunkt = start.AddHours(2), Plats = "Örebro", Meddelande = "Sorterad", Latitud = 59.2753, Longitud = 15.2134}
                },
                "Örebro" => new List<SpårningsPunkt>
                {
                    new SpårningsPunkt {Tidpunkt = start.AddHours(4), Plats = "Örebro (Ombud)", Meddelande = "Klar för avhämtning", Latitud = 59.2753, Longitud = 15.2134 },
                    new SpårningsPunkt {Tidpunkt = DateTime.Now.AddHours(-1), Plats = "Örebro (Lager)", Meddelande = "Utkörd från lager", Latitud = 59.2753, Longitud = 15.2134}
                },
                _ => new List<SpårningsPunkt>
                {
                    new SpårningsPunkt {Tidpunkt = start.AddHours(8), Plats = kund.Stad, Meddelande = "Framme vid sortering", Latitud = HämtaLat(kund.Stad), Longitud = HämtaLng(kund.Stad) }
                }
            };
        }

        public async Task<Frakt> BokaFrakt(int orderId, FraktAlternativ valtAlternativ)
        {
            await Task.Delay(400);

            return new Frakt
            {
                OrderID = orderId,
                Transportör = valtAlternativ.Namn,
                Pris = valtAlternativ.Pris, 
                Status = "Beställd",
                StartDatum = DateTime.Now,
                Sändningsnummer = "HATT-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                KolliId = "KID-" + new Random().Next(1000, 9999)

            };
        }

        public async Task<List<FraktAlternativ>> HämtaFraktAlternativ(string land)
        {
            await Task.Delay(400);

            bool utrikes = !string.Equals(land, "Sverige", StringComparison.OrdinalIgnoreCase);

            if (utrikes)
            {
                return new List<FraktAlternativ>
                {
                    new FraktAlternativ { Namn = "DHL International", Pris = 450, LeveransTid = 3 },
                    new FraktAlternativ { Namn = "UPS Worldwide", Pris = 595, LeveransTid = 2 },
                    new FraktAlternativ { Namn = "Postnord International", Pris = 280, LeveransTid = 5 }
                };
            }
            

            return new List<FraktAlternativ>
            {
                new FraktAlternativ { Namn = "DHL Express", Pris = 250, LeveransTid = 1 },
                new FraktAlternativ { Namn = "Schenker Standard", Pris = 79, LeveransTid = 4 },
                new FraktAlternativ { Namn = "Postnord MyPack", Pris = 120, LeveransTid = 2 }
            };
        }

        private double HämtaLat(string stad) => stad switch
        {
            "Stockholm" => 59.3293,
            "Örebro" => 59.2753,
            "Helsingfors" => 60.1695,
            _ => 59.3293
        };

        private double HämtaLng(string stad) => stad switch
        {
            "Stockholm" => 18.0686,
            "Örebro" => 15.2134,
            "Helsingfors" => 24.9354,
            _ => 18.0686
        };

    }
}
