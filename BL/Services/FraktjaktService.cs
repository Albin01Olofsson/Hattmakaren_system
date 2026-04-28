using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;
using Microsoft.Identity.Client;
using Models;

namespace BL.Services
{
    public class FraktjaktService : IFraktjaktService
    {
        private readonly IOrderRepository _orderRepo;

        public FraktjaktService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public async Task<List<SpårningsPunkt>> HämtaHistorik(string sändningsnummer)
        {
            //simulering fördröjning
            await Task.Delay(400);

            var frakt = await _orderRepo.GetFraktBySändningsnummer(sändningsnummer);

            if (frakt == null || frakt.Status == "Beställd" || frakt.Status == "Plockas")
            {
                return new List<SpårningsPunkt>
                {
                    new SpårningsPunkt
                    {
                        Tidpunkt = frakt?.StartDatum ?? DateTime.Now,
                        Plats = "Lager (Butik)",
                        Meddelande = frakt.Status,
                        Latitud = 59.2662,
                        Longitud = 15.2104
                    }
                };
            }

            int ruttVal = (Math.Abs(sändningsnummer.GetHashCode()) % 5) + 1;

            switch (ruttVal)
            {
                case 1:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-20), Plats = "Malmö Terminal", Meddelande = "Ankommit destination", Latitud = 55.6050, Longitud = 13.0038 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-5), Plats = "Värnamo", Meddelande = "Passerat kontrollstation", Latitud = 57.1837, Longitud = 14.0463 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Stockholm", Meddelande = "Sorterad", Latitud = 59.3293, Longitud = 18.0686 }
                    };
                case 2:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-45), Plats = "Umeå Logistikcenter", Meddelande = "Lastas på bil", Latitud = 63.8258, Longitud = 20.2630 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-8), Plats = "Sundsvall", Meddelande = "I transit", Latitud = 62.3908, Longitud = 17.3069 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Stockholm", Meddelande = "Lämnat terminal", Latitud = 59.3293, Longitud = 18.0686 }
                    };
                case 3:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-10), Plats = "Göteborg Hamn", Meddelande = "Klar för utlämning", Latitud = 57.7089, Longitud = 11.9746 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-4), Plats = "Alingsås", Meddelande = "Transport påbörjad", Latitud = 57.9300, Longitud = 12.5300 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Stockholm", Meddelande = "Registrerad", Latitud = 59.3293, Longitud = 18.0686 }
                    };
                case 4:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-5), Plats = "Solna", Meddelande = "Ute för leverans", Latitud = 59.3689, Longitud = 18.0084 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-1), Plats = "Bromma", Meddelande = "Sorterad", Latitud = 59.3386, Longitud = 17.9419 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-3), Plats = "Stockholm City", Meddelande = "Inlämnad", Latitud = 59.3293, Longitud = 18.0686 }
                    };
                case 5:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-15), Plats = "Berlin, DE", Meddelande = "Ankommit till kundens lokala utlämningsställe", Latitud = 52.5200, Longitud = 13.4050},
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-6), Plats = "Hamburg Hub", Meddelande = "Sorterad i Tyskland", Latitud = 53.5511, Longitud = 9.9937 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Malmö, SE", Meddelande = "Lämnat Sverige via Öresundsbron", Latitud = 55.6050, Longitud = 13.0038 }
                    };
                case 6:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-5), Plats = "New York, NY", Meddelande = "Genomgår tullkontroll (JFK)", Latitud = 40.7128, Longitud = -74.0060 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddHours(-12), Plats = "Atlantic Ocean", Meddelande = "I luften - Beräknad landning om 2h", Latitud = 50.0000, Longitud = -30.0000 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Arlanda Airport, SE", Meddelande = "Lastad på flygfrakt", Latitud = 59.6498, Longitud = 17.9238 }
                    };
                default:
                    return new List<SpårningsPunkt>
                    {
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddMinutes(-30), Plats = "Örebro", Meddelande = "Ankommit sortering", Latitud = 59.2753, Longitud = 15.2134 },
                        new SpårningsPunkt { Tidpunkt = DateTime.Now.AddDays(-1), Plats = "Västerås", Meddelande = "Skickad", Latitud = 59.6099, Longitud = 16.5448 }
                    };
            }

            
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

            return new List<FraktAlternativ>
            {
                new FraktAlternativ { Namn = "DHL Express", Pris = 250, LeveransTid = 1 },
                new FraktAlternativ { Namn = "Schenker Standard", Pris = 79, LeveransTid = 4 },
                new FraktAlternativ { Namn = "Postnord MyPack", Pris = 120, LeveransTid = 2 }
            };
        }

    }
}
