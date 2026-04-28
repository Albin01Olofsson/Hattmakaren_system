using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using Microsoft.EntityFrameworkCore;
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
        private readonly DBcontext _context;

        private List<Material> _allaMaterial = new();
        private List<Produkt> _allaProdukter = new();
        private List<Kund> _allaKunder = new();
        private List<Order> _allaOrdrar = new();
        private List<MaterialBeställning> _allaMaterialBestallningar = new();

        public ObservableCollection<StatistikKort> Nyckeltal { get; } = new();
        public ObservableCollection<HattStatistikRad> Hattar { get; } = new();
        public ObservableCollection<KundStatistikRad> Kunder { get; } = new();
        public ObservableCollection<MaterialStatistikRad> Material { get; } = new();
        public ObservableCollection<DetaljRad> HattDetaljer { get; } = new();
        public ObservableCollection<DetaljRad> KundDetaljer { get; } = new();
        public ObservableCollection<DetaljRad> MaterialDetaljer { get; } = new();

        [ObservableProperty]
        private string uppdateradText = string.Empty;

        [ObservableProperty]
        private string periodText = "Alla datum";

        [ObservableProperty]
        private DateTime? datumFran;

        [ObservableProperty]
        private DateTime? datumTill;

        [ObservableProperty]
        private HattStatistikRad? valdHatt;

        [ObservableProperty]
        private KundStatistikRad? valdKund;

        [ObservableProperty]
        private MaterialStatistikRad? valtMaterial;

        public StatistikViewModel(
            IMaterialService materialService,
            IProduktService produktService,
            IKundService kundService,
            IOrderService orderService,
            DBcontext context)
        {
            _materialService = materialService;
            _produktService = produktService;
            _kundService = kundService;
            _orderService = orderService;
            _context = context;

            _ = LaddaStatistik();
        }

        partial void OnDatumFranChanged(DateTime? value) => UppdateraStatistik();

        partial void OnDatumTillChanged(DateTime? value) => UppdateraStatistik();

        partial void OnValdHattChanged(HattStatistikRad? value) => FyllHattDetaljer();

        partial void OnValdKundChanged(KundStatistikRad? value) => FyllKundDetaljer();

        partial void OnValtMaterialChanged(MaterialStatistikRad? value) => FyllMaterialDetaljer();

        private async Task LaddaStatistik()
        {
            _allaMaterial = await _materialService.GetMaterialLista();
            _allaProdukter = await _produktService.GetProdukter();
            _allaKunder = (await _kundService.HämtaAllaKunder())
                .Where(k => k.Namn != "Borttagen kund")
                .ToList();
            _allaOrdrar = await _orderService.GetOrdersWithNavProps();
            _allaMaterialBestallningar = await _context.MaterialBeställningar
                .Include(b => b.Rader)
                .ThenInclude(r => r.Material)
                .ToListAsync();

            UppdateraStatistik();
        }

        private void UppdateraStatistik()
        {
            var ordrar = FiltreraOrdrar();
            var saldaRader = HamtaSaldaRader(ordrar);
            var materialBestallningar = FiltreraMaterialBestallningar();

            FyllNyckeltal(ordrar, saldaRader, materialBestallningar);
            FyllHattar(saldaRader);
            FyllKunder(ordrar);
            FyllMaterial(materialBestallningar, saldaRader);

            ValdHatt = Hattar.FirstOrDefault();
            ValdKund = Kunder.FirstOrDefault();
            ValtMaterial = Material.FirstOrDefault();

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

        private List<MaterialBeställning> FiltreraMaterialBestallningar()
        {
            var resultat = _allaMaterialBestallningar.AsEnumerable();

            if (DatumFran.HasValue)
            {
                resultat = resultat.Where(b => b.Datum.HasValue && b.Datum.Value.Date >= DatumFran.Value.Date);
            }

            if (DatumTill.HasValue)
            {
                resultat = resultat.Where(b => b.Datum.HasValue && b.Datum.Value.Date <= DatumTill.Value.Date);
            }

            return resultat.ToList();
        }

        private List<SaldOrderRad> HamtaSaldaRader(List<Order> ordrar)
        {
            var rader = new List<SaldOrderRad>();

            foreach (var order in ordrar)
            {
                foreach (var orderRad in order.OrderRader)
                {
                    if (orderRad.Produkt == null)
                    {
                        continue;
                    }

                    rader.Add(new SaldOrderRad(order, orderRad.Produkt, orderRad.Antal));
                }
            }

            return rader;
        }

        private void FyllNyckeltal(List<Order> ordrar, List<SaldOrderRad> saldaRader, List<MaterialBeställning> materialBestallningar)
        {
            Nyckeltal.Clear();

            var antalHattar = saldaRader.Sum(r => r.Antal);
            var antalKunder = ordrar.Select(o => o.KundID).Distinct().Count();
            var totalIntakt = ordrar.Sum(o => o.Pris);
            var mestSald = saldaRader
                .GroupBy(r => r.Produkt.Namn)
                .OrderByDescending(g => g.Sum(r => r.Antal))
                .FirstOrDefault();
            var materialStatistik = SkapaMaterialStatistik(materialBestallningar, saldaRader);
            var mestMaterial = materialStatistik
                .OrderByDescending(m => m.AntalAnvand)
                .ThenByDescending(m => m.AntalBestallt)
                .FirstOrDefault();

            Nyckeltal.Add(new StatistikKort("Sålda hattar", antalHattar.ToString(), $"{saldaRader.Select(r => r.Produkt.ProduktID).Distinct().Count()} olika", "#FF2ECC71"));
            Nyckeltal.Add(new StatistikKort("Ordrar", ordrar.Count.ToString(), $"{antalKunder} kunder", "#FFC5A059"));
            Nyckeltal.Add(new StatistikKort("Intäkt", $"{totalIntakt:0} kr", "I perioden", "#FF4A90A4"));
            Nyckeltal.Add(new StatistikKort("Mest såld", mestSald?.Key ?? "Ingen data", $"{mestSald?.Sum(r => r.Antal) ?? 0} st", "#FFF59E0B"));
            Nyckeltal.Add(new StatistikKort("Mest använt", mestMaterial?.Namn ?? "Ingen data", mestMaterial == null ? "0 hattar" : $"{mestMaterial.AntalAnvand} hattar", "#FFE74C3C"));
        }

        private void FyllHattar(List<SaldOrderRad> saldaRader)
        {
            Hattar.Clear();

            var grupper = saldaRader
                .GroupBy(r => r.Produkt.ProduktID)
                .Select(g => new HattStatistikRad(
                    g.Key,
                    TomEllerVarde(g.First().Produkt.Namn, "Okänd hatt"),
                    TomEllerVarde(g.First().Produkt.HattTyp, "Okänd typ"),
                    g.Sum(r => r.Antal),
                    g.Sum(r => r.Produkt.Pris * r.Antal),
                    g.Select(r => r.Order.KundID).Distinct().Count()))
                .OrderByDescending(h => h.AntalSalda)
                .ThenBy(h => h.Namn)
                .ToList();

            foreach (var hatt in grupper)
            {
                Hattar.Add(hatt);
            }
        }

        private void FyllKunder(List<Order> ordrar)
        {
            Kunder.Clear();

            var grupper = ordrar
                .Where(o => o.Kund != null && o.Kund.Namn != "Borttagen kund")
                .GroupBy(o => o.KundID)
                .Select(g => new KundStatistikRad(
                    g.Key,
                    g.First().Kund.Namn,
                    g.Count(),
                    g.Sum(o => o.OrderRader.Sum(r => r.Antal)),
                    g.Sum(o => o.Pris)))
                .OrderByDescending(k => k.AntalHattar)
                .ThenBy(k => k.Namn)
                .ToList();

            foreach (var kund in grupper)
            {
                Kunder.Add(kund);
            }
        }

        private void FyllMaterial(List<MaterialBeställning> materialBestallningar, List<SaldOrderRad> saldaRader)
        {
            Material.Clear();

            var grupper = SkapaMaterialStatistik(materialBestallningar, saldaRader);

            foreach (var material in grupper)
            {
                Material.Add(material);
            }
        }

        private List<MaterialStatistikRad> SkapaMaterialStatistik(List<MaterialBeställning> materialBestallningar, List<SaldOrderRad> saldaRader)
        {
            var materialRader = RaknaMaterial(materialBestallningar);
            var anvandaMaterial = RaknaMaterialFranSaldaHattar(saldaRader);

            return _allaMaterial
                .Select(material =>
                {
                    var rader = materialRader
                        .Where(r => r.Material.MaterialID == material.MaterialID)
                        .ToList();
                    var saldaHattar = anvandaMaterial
                        .Where(r => r.Material.MaterialID == material.MaterialID)
                        .ToList();
                    var bestallt = rader.Sum(r => r.Bestallt);
                    var anvant = saldaHattar.Sum(r => r.Antal);

                    return new MaterialStatistikRad(
                        material.MaterialID,
                        material.Namn,
                        material.MåttText,
                        bestallt,
                        anvant,
                        material.Lagerantal,
                        material.Pris * material.Lagerantal,
                        saldaHattar.Select(r => r.ProduktID).Distinct().Count());
                })
                .OrderByDescending(m => m.AntalAnvand)
                .ThenByDescending(m => m.AntalBestallt)
                .ThenBy(m => m.Namn)
                .ToList();
        }

        //private List<SaldMaterialRad> RaknaMaterialFranSaldaHattar(List<SaldOrderRad> saldaRader)
        //{
        //    var resultat = new List<SaldMaterialRad>();

        //    foreach (var rad in saldaRader)
        //    {
        //        var produkt = _allaProdukter.FirstOrDefault(p => p.ProduktID == rad.Produkt.ProduktID);

        //        if (produkt == null || produkt.MaterialLista == null)
        //        {
        //            continue;
        //        }

        //        foreach (var material in produkt.MaterialLista)
        //        {
        //            resultat.Add(new SaldMaterialRad(
        //                produkt.ProduktID,
        //                produkt.Namn,
        //                material,
        //                rad.Antal));
        //        }
        //    }

        //    return resultat;
        //}
        private List<SaldMaterialRad> RaknaMaterialFranSaldaHattar(List<SaldOrderRad> saldaRader)
        {
            var resultat = new List<SaldMaterialRad>();

            foreach (var rad in saldaRader)
            {
                var produkt = _allaProdukter
                    .FirstOrDefault(p => p.ProduktID == rad.Produkt.ProduktID);

                if (produkt == null || produkt.ProduktMaterial == null)
                    continue;

                foreach (var pm in produkt.ProduktMaterial)
                {
                    if (pm.Material == null)
                        continue;

                    resultat.Add(new SaldMaterialRad(
                        produkt.ProduktID,
                        produkt.Namn,
                        pm.Material,
                        (int)pm.Mängd * rad.Antal   
                    ));
                }
            }

            return resultat;
        }

        private List<MaterialBestalldRad> RaknaMaterial(List<MaterialBeställning> materialBestallningar)
        {
            var resultat = new List<MaterialBestalldRad>();

            foreach (var bestallning in materialBestallningar)
            {
                foreach (var rad in bestallning.Rader)
                {
                    if (rad.Material == null)
                    {
                        continue;
                    }

                    resultat.Add(new MaterialBestalldRad(
                        bestallning.MaterialBeställningID,
                        bestallning.Leverantör,
                        rad.Material,
                        rad.Antal));
                }
            }

            return resultat;
        }

        private void FyllHattDetaljer()
        {
            HattDetaljer.Clear();

            if (ValdHatt == null)
            {
                return;
            }

            var rader = HamtaSaldaRader(FiltreraOrdrar())
                .Where(r => r.Produkt.ProduktID == ValdHatt.ProduktID)
                .GroupBy(r => r.Order.Kund?.Namn ?? "Okänd kund")
                .Select(g => new DetaljRad(g.Key, $"{g.Sum(r => r.Antal)} st", "Kund"))
                .OrderByDescending(r => r.Varde)
                .ToList();

            foreach (var rad in rader)
            {
                HattDetaljer.Add(rad);
            }
        }

        private void FyllKundDetaljer()
        {
            KundDetaljer.Clear();

            if (ValdKund == null)
            {
                return;
            }

            var rader = FiltreraOrdrar()
                .Where(o => o.KundID == ValdKund.KundID)
                .SelectMany(o => o.OrderRader)
                .Where(r => r.Produkt != null)
                .GroupBy(r => r.Produkt.Namn)
                .Select(g => new DetaljRad(g.Key, $"{g.Sum(r => r.Antal)} st", "Hatt"))
                .OrderBy(r => r.Namn)
                .ToList();

            foreach (var rad in rader)
            {
                KundDetaljer.Add(rad);
            }
        }

        private void FyllMaterialDetaljer()
        {
            MaterialDetaljer.Clear();

            if (ValtMaterial == null)
            {
                return;
            }

            var materialIProdukter = RaknaMaterialFranSaldaHattar(HamtaSaldaRader(FiltreraOrdrar()))
                .Where(m => m.Material.MaterialID == ValtMaterial.MaterialID)
                .GroupBy(m => TomEllerVarde(m.ProduktNamn, "Okänd hatt"))
                .Select(g => new DetaljRad(g.Key, $"{g.Sum(m => m.Antal)} hattar", "Hatt"))
                .OrderByDescending(r => r.Varde)
                .ToList();

            foreach (var rad in materialIProdukter)
            {
                MaterialDetaljer.Add(rad);
            }

            var rader = RaknaMaterial(FiltreraMaterialBestallningar())
                .Where(m => m.Material.MaterialID == ValtMaterial.MaterialID)
                .GroupBy(m => TomEllerVarde(m.Leverantör, "Okänd leverantör"))
                .Select(g => new DetaljRad(g.Key, $"{g.Sum(m => m.Antal)} {ValtMaterial.Matt}", "Leverantör"))
                .OrderByDescending(r => r.Varde)
                .ToList();

            foreach (var rad in rader)
            {
                MaterialDetaljer.Add(rad);
            }

            if (!MaterialDetaljer.Any())
            {
                MaterialDetaljer.Add(new DetaljRad("Nuvarande lager", ValtMaterial.LagerText, "Lager"));
            }
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

    public class HattStatistikRad
    {
        public HattStatistikRad(int produktID, string namn, string typ, int antalSalda, decimal intakt, int antalKunder)
        {
            ProduktID = produktID;
            Namn = namn;
            Typ = typ;
            AntalSalda = antalSalda;
            Intakt = intakt;
            AntalKunder = antalKunder;
        }

        public int ProduktID { get; }
        public string Namn { get; }
        public string Typ { get; }
        public int AntalSalda { get; }
        public decimal Intakt { get; }
        public int AntalKunder { get; }
        public string IntaktText => $"{Intakt:0} kr";
    }

    public class KundStatistikRad
    {
        public KundStatistikRad(int kundID, string namn, int antalOrdrar, int antalHattar, decimal totaltKopt)
        {
            KundID = kundID;
            Namn = namn;
            AntalOrdrar = antalOrdrar;
            AntalHattar = antalHattar;
            TotaltKopt = totaltKopt;
        }

        public int KundID { get; }
        public string Namn { get; }
        public int AntalOrdrar { get; }
        public int AntalHattar { get; }
        public decimal TotaltKopt { get; }
        public string TotaltKoptText => $"{TotaltKopt:0} kr";
    }

    public class MaterialStatistikRad
    {
        public MaterialStatistikRad(int materialID, string namn, string matt, int antalBestallt, int antalAnvand, int lagerantal, decimal uppskattatVarde, int antalProdukter)
        {
            MaterialID = materialID;
            Namn = namn;
            Matt = matt;
            AntalBestallt = antalBestallt;
            AntalAnvand = antalAnvand;
            Lagerantal = lagerantal;
            UppskattatVarde = uppskattatVarde;
            AntalProdukter = antalProdukter;
        }

        public int MaterialID { get; }
        public string Namn { get; }
        public string Matt { get; }
        public int AntalBestallt { get; }
        public int AntalAnvand { get; }
        public int Lagerantal { get; }
        public decimal UppskattatVarde { get; }
        public int AntalProdukter { get; }
        public string BestalltText => $"{AntalBestallt} {Matt}";
        public string AnvantText => $"{AntalAnvand} st";
        public string LagerText => $"{Lagerantal} {Matt}";
        public string UppskattatVardeText => $"{UppskattatVarde:0} kr";
    }

    public class DetaljRad
    {
        public DetaljRad(string namn, string varde, string typ)
        {
            Namn = namn;
            Varde = varde;
            Typ = typ;
        }

        public string Namn { get; }
        public string Varde { get; }
        public string Typ { get; }
    }

    public class SaldOrderRad
    {
        public SaldOrderRad(Order order, Produkt produkt, int antal)
        {
            Order = order;
            Produkt = produkt;
            Antal = antal;
        }

        public Order Order { get; }
        public Produkt Produkt { get; }
        public int Antal { get; }
    }

    public class SaldMaterialRad
    {
        public SaldMaterialRad(int produktID, string produktNamn, Material material, int antal)
        {
            ProduktID = produktID;
            ProduktNamn = produktNamn;
            Material = material;
            Antal = antal;
        }

        public int ProduktID { get; }
        public string ProduktNamn { get; }
        public Material Material { get; }
        public int Antal { get; }
    }

    public class MaterialBestalldRad
    {
        public MaterialBestalldRad(int materialBeställningID, string leverantör, Material material, int antal)
        {
            MaterialBeställningID = materialBeställningID;
            Leverantör = leverantör;
            Material = material;
            Antal = antal;
        }

        public int MaterialBeställningID { get; }
        public string Leverantör { get; }
        public Material Material { get; }
        public int Antal { get; }
        public int Bestallt => Antal;
        public int Anvant => 0;
    }
}
