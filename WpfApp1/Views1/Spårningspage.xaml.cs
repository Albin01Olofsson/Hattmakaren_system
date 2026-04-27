using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.ViewModels;
using Models;
using System.Threading.Tasks;
using DAL;
using BL.Services;
using DAL.Repositorys;


namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for spårningPage.xaml
    /// </summary>
    public partial class SpårningPage : Page
    {
        public SpårningPage()
        {
            InitializeComponent();

            var service = new OrderService(new OrderRepo(new DBcontext()),  new DBcontext());
            var vm = new SpårningViewModel(service);

            this.DataContext = vm;

            vm.SkapaTestOrder();

            //Tvinga windows anv säkerhet vid anrop
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //Identifiera oss som webbläsare
            GMap.NET.MapProviders.GMapProvider.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

            //Anv OpenStreetMap
            MainMap.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;

            //rensa gamla inställningar
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Lokalisering kordinater just nu hårdkodat till sthlm
            MainMap.Position = new GMap.NET.PointLatLng(59.3293, 18.0686);

            //zoom (OBS! viktig placering Min->Max->Startpos)
            MainMap.MinZoom = 2;
            MainMap.MaxZoom = 18;
            MainMap.Zoom = 13;

            //Interaktioner
            MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            MainMap.CanDragMap = true;
            MainMap.DragButton = System.Windows.Input.MouseButton.Left;

            MainMap.UpdateLayout();

            AdderaLeveransMarkör(59.3293, 18.0686, "Hatt-leverans #1: Slottet", true);
            AdderaLeveransMarkör(59.3326, 18.0645, "Hatt-leverans #2: Centralen", false);
            AdderaLeveransMarkör(59.3385, 18.0335, "Hatt-leverans #3: S:t Eriksplan", false);
        }

        private void AdderaLeveransMarkör(double lat, double lng, string info, bool ärLevererad)
        {
            //Ge markören kordinaterna
            GMapMarker marker = new GMapMarker(new GMap.NET.PointLatLng(lat, lng));

            //färg baserat på status
            Color statusF = ärLevererad ? Color.FromRgb(39, 174, 96) : Color.FromRgb(197, 160, 89);

            //utseendet
            Border transportHalo = new Border
            {
                Width = 40,
                Height = 40,
                Background = new SolidColorBrush(statusF),
                CornerRadius = new CornerRadius(20),
                Effect = new System.Windows.Media.Effects.DropShadowEffect { BlurRadius = 5, Opacity = 0.5 },
                Child = new TextBlock
                {
                    Text = "🚚",
                    FontSize = 22,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                },
                ToolTip = info
            };

            transportHalo.MouseEnter += (s, e) =>
            {
                transportHalo.Width = 50;
                transportHalo.Height = 50;
            };

            transportHalo.MouseLeave += (s, e) =>
            {
                transportHalo.Height = 40;
                transportHalo.Width = 40;
            };

            transportHalo.MouseDown += (s, e) =>
            {
                MessageBox.Show($"Info: {info}");
            };

            //koppla txtblock tikk markör
            marker.Shape = transportHalo;

            //justerar pos för centrering
            marker.Offset = new System.Windows.Point(-20, -20);

            //Lägg till i kartans samling
            MainMap.Markers.Add(marker);
        }

        private void FokuseraPåPosition(double lat, double lng, int zoomNivå = 13)
        {
            MainMap.Position = new GMap.NET.PointLatLng(lat, lng);
            MainMap.Zoom = zoomNivå;
        }

        //Återgångsknapp till senaste markör
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SpårningViewModel)this.DataContext;

            
        }

        private void AdderaAdressMarkör(string adress, string info)
        {
            GeoCoderStatusCode status;
            PointLatLng? position = GMapProviders.GoogleHybridMap.GetPoint(adress, out status);


            if (status == GeoCoderStatusCode.G_GEO_SUCCESS && position.HasValue)
            {
                AdderaLeveransMarkör(position.Value.Lat, position.Value.Lng, info, false);
            }
            else
            {
                MessageBox.Show("Kunde inte hitta adressen: " + status.ToString());
            }
        }

        private async void Leveranslista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var vm = (SpårningViewModel)this.DataContext;

            if (Leveranslista.SelectedItem is Order valdOrder)
            {
                var frakt = valdOrder.Frakt.FirstOrDefault();

                if (frakt != null)
                {
                    await vm.HämtaHistorikFrånFraktjakt(frakt.Sändningsnummer);

                    var senaste = vm.Uppdateringar.FirstOrDefault();

                    if (senaste != null)
                    {
                        MainMap.Markers.Clear();
                        AdderaLeveransMarkör(senaste.Latitud, senaste.Longitud, $"{senaste.Plats}: {senaste.Meddelande}", false);

                        FokuseraPåPosition(senaste.Latitud, senaste.Longitud, 12);
                    }

                }
            }

        }

        private void HändelserLista_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is ListBox lb && lb.SelectedItem is SpårningsPunkt punkt)
            {
                FokuseraPåPosition(punkt.Latitud, punkt.Longitud, 12);

                MainMap.Markers.Clear();
                AdderaLeveransMarkör(punkt.Latitud, punkt.Longitud, punkt.Plats, false);
            }
        }
    }
}