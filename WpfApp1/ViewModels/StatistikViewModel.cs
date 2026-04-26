using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class StatistikViewModel : ObservableObject
    {
        private readonly IMaterialService _materialService;
        private readonly IProduktService _produktService;
        private readonly IKundService _kundService;
        private readonly IOrderService _orderService;

        private List<Material> _allaMaterial = new();
        private List<Produkt> _allaProdukter = new();
        private List<Kund> _allaKunder = new();
        private List<Order> _allaOrdrar = new();

        public ObservableCollection<StatistikKort> Nyckeltal { get; } = new();
        public ObservableCollection<StatistikRad> MaterialStatistik { get; } = new();
        public ObservableCollection<StatistikRad> HattStatistik { get; } = new();
        public ObservableCollection<StatistikRad> KundStatistik { get; } = new();

        [ObservableProperty]
        private string uppdateradText = string.Empty;

        [ObservableProperty]
        private string periodText = "Alla datum";

        [ObservableProperty]
        private DateTime? datumFran;

        [ObservableProperty]
        private DateTime? datumTill;

        public StatistikViewModel(
            IMaterialService materialService,
            IProduktService produktService,
            IKundService kundService,
            IOrderService orderService)
        {
            _materialService = materialService;
            _produktService = produktService;
            _kundService = kundService;
            _orderService = orderService;

            _ = LaddaStatistik();
        }

        partial void OnDatumFranChanged(DateTime? value) => UppdateraStatistik();

        partial void OnDatumTillChanged(DateTime? value) => UppdateraStatistik();

        private async Task LaddaStatistik()
        {
            _allaMaterial = await _materialService.GetMaterialLista();
            _allaProdukter = await _produktService.GetProdukter();
            _allaKunder = (await _kundService.HämtaAllaKunder())
                .Where(k => k.Namn != "Borttagen kund")
                .ToList();
            _allaOrdrar = await _orderService.GetOrdersWithNavProps();

            UppdateraStatistik();
        }

        private void UppdateraStatistik()
        {
            var filtreradeOrdrar = FiltreraOrdrar();
            var filtreradeProdukter = HamtaProdukterForPeriod(filtreradeOrdrar);

            FyllNyckeltal(_allaMaterial, filtreradeProdukter, _allaKunder, filtreradeOrdrar);
            FyllMaterialStatistik(_allaMaterial);
            FyllHattStatistik(filtreradeProdukter);
            FyllKundStatistik(_allaKunder, filtreradeOrdrar);

            PeriodText = SkapaPeriodText();
            UppdateradText = $"Uppdaterad {DateTime.Now:yyyy-MM-dd HH:mm}";
        }

        private List<Order> FiltreraOrdrar()
        {
            var resultat = _allaOrdrar.AsEnumerable();

            if (DatumFran.HasValue)
            {
                resultat = resultat.Where(o => o.Datum.Date >= DatumFran.Value.Date);
            }

            if (DatumTill.HasValue)
            {
                resultat = resultat.Where(o => o.Datum.Date <= DatumTill.Value.Date);
            }

            return resultat.ToList();
        }

        private List<Produkt> HamtaProdukterForPeriod(List<Order> filtreradeOrdrar)
        {
            if (!DatumFran.HasValue && !DatumTill.HasValue)
            {
                return _allaProdukter;
            }

            var produkter = new List<Produkt>();

            foreach (var order in filtreradeOrdrar)
            {
                foreach (var orderRad in order.OrderRader)
                {
                    if (orderRad.Produkt == null)
                    {
                        continue;
                    }

                    for (int i = 0; i < orderRad.Antal; i++)
                    {
                        produkter.Add(orderRad.Produkt);
                    }
                }
            }

            return produkter;
        }

        private string SkapaPeriodText()
        {
            if (DatumFran.HasValue && DatumTill.HasValue)
            {
                return $"{DatumFran.Value:yyyy-MM-dd} till {DatumTill.Value:yyyy-MM-dd}";
            }

            if (DatumFran.HasValue)
            {
                return $"Från {DatumFran.Value:yyyy-MM-dd}";
            }

            if (DatumTill.HasValue)
            {
                return $"Till {DatumTill.Value:yyyy-MM-dd}";
            }

            return "Alla datum";
        }

        [RelayCommand]
        private void RensaDatumFilter()
        {
            DatumFran = null;
            DatumTill = null;
        }

        private void FyllNyckeltal(List<Material> material, List<Produkt> produkter, List<Kund> kunder, List<Order> ordrar)
        {
            Nyckeltal.Clear();

            var materialVarde = material.Sum(m => m.Pris * m.Lagerantal);
            var lagerProdukter = produkter.OfType<LagerfördProdukt>().ToList();
            var specialProdukter = produkter.OfType<SpecialBeställning>().ToList();
            var totalIntakt = ordrar.Sum(o => o.Pris);
            var snittOrder = ordrar.Any() ? ordrar.Average(o => o.Pris) : 0;

            Nyckeltal.Add(new StatistikKort("Intäkt", $"{totalIntakt:0} kr", $"{ordrar.Count} ordrar i perioden", "#FFC5A059"));
            Nyckeltal.Add(new StatistikKort("Snittorder", $"{snittOrder:0} kr", $"{ordrar.Count(o => o.IsPrio)} prioriterade", "#FF4A90A4"));
            Nyckeltal.Add(new StatistikKort("Hattar", produkter.Count.ToString(), $"{lagerProdukter.Count} lagerförda, {specialProdukter.Count} special", "#FF2ECC71"));
            Nyckeltal.Add(new StatistikKort("Kunder", kunder.Count.ToString(), $"{ordrar.Select(o => o.KundID).Distinct().Count()} aktiva i perioden", "#FFF59E0B"));
            Nyckeltal.Add(new StatistikKort("Materialvärde", $"{materialVarde:0} kr", $"{material.Count(m => m.Lagerantal <= 3)} med lågt lager", "#FFE74C3C"));
        }

        private void FyllMaterialStatistik(List<Material> material)
        {
            MaterialStatistik.Clear();
            var grupper = material
                .GroupBy(m => TomEllerVarde(m.Namn, "Okänt material"))
                .OrderByDescending(g => g.Count())
                .ToList();
            var maxAntal = Math.Max(1, grupper.Any() ? grupper.Max(g => g.Count()) : 1);

            foreach (var grupp in grupper)
            {
                var lagerSumma = grupp.Sum(m => m.Lagerantal);
                var varde = grupp.Sum(m => m.Pris * m.Lagerantal);

                MaterialStatistik.Add(new StatistikRad(
                    grupp.Key,
                    grupp.Count(),
                    $"{lagerSumma} i lager",
                    $"Värde {varde:0} kr",
                    RaknaBredd(grupp.Count(), maxAntal)));
            }
        }

        private void FyllHattStatistik(List<Produkt> produkter)
        {
            HattStatistik.Clear();
            var grupper = produkter
                .GroupBy(p => TomEllerVarde(p.HattTyp, "Okänd typ"))
                .OrderByDescending(g => g.Count())
                .ToList();
            var maxAntal = Math.Max(1, grupper.Any() ? grupper.Max(g => g.Count()) : 1);

            foreach (var grupp in grupper)
            {
                var snittPris = grupp.Any() ? grupp.Average(p => p.Pris) : 0;
                var fardiga = grupp.Count(p => p.Färdig);

                HattStatistik.Add(new StatistikRad(
                    grupp.Key,
                    grupp.Count(),
                    $"{fardiga} färdiga",
                    $"Snittpris {snittPris:0} kr",
                    RaknaBredd(grupp.Count(), maxAntal)));
            }

            if (!HattStatistik.Any())
            {
                HattStatistik.Add(new StatistikRad("Inga hattar i perioden", 0, "Ändra datumfilter", "Ingen ordermatchning", 0));
            }
        }

        private void FyllKundStatistik(List<Kund> kunder, List<Order> ordrar)
        {
            KundStatistik.Clear();

            var orderPerKund = ordrar
                .Where(o => o.Kund != null && o.Kund.Namn != "Borttagen kund")
                .GroupBy(o => o.Kund.Namn)
                .Select(g => new
                {
                    KundNamn = g.Key,
                    Antal = g.Count(),
                    Intakt = g.Sum(o => o.Pris)
                })
                .OrderByDescending(k => k.Antal)
                .ThenByDescending(k => k.Intakt)
                .ToList();

            var maxAntal = Math.Max(1, orderPerKund.Any() ? orderPerKund.Max(k => k.Antal) : 1);

            foreach (var kund in orderPerKund.Take(8))
            {
                KundStatistik.Add(new StatistikRad(
                    kund.KundNamn,
                    kund.Antal,
                    $"{kund.Intakt:0} kr totalt",
                    "Ordrar i vald period",
                    RaknaBredd(kund.Antal, maxAntal)));
            }

            if (!KundStatistik.Any())
            {
                KundStatistik.Add(new StatistikRad("Inga kundordrar", kunder.Count, "Inga ordrar i perioden", "Ändra datumfilter", 0));
            }
        }

        private static double RaknaBredd(int antal, int maxAntal)
        {
            return maxAntal == 0 ? 0 : Math.Max(18, 220.0 * antal / maxAntal);
        }

        private static string TomEllerVarde(string? varde, string standard)
        {
            return string.IsNullOrWhiteSpace(varde) ? standard : varde.Trim();
        }
    }

    public class StatistikKort
    {
        public StatistikKort(string rubrik, string varde, string detalj, string accentFarg)
        {
            Rubrik = rubrik;
            Varde = varde;
            Detalj = detalj;
            AccentFarg = accentFarg;
        }

        public string Rubrik { get; }
        public string Varde { get; }
        public string Detalj { get; }
        public string AccentFarg { get; }
    }

    public class StatistikRad
    {
        public StatistikRad(string rubrik, int antal, string detalj, string sekundarDetalj, double bredd)
        {
            Rubrik = rubrik;
            Antal = antal;
            Detalj = detalj;
            SekundarDetalj = sekundarDetalj;
            Bredd = bredd;
        }

        public string Rubrik { get; }
        public int Antal { get; }
        public string Detalj { get; }
        public string SekundarDetalj { get; }
        public double Bredd { get; }
    }
}
