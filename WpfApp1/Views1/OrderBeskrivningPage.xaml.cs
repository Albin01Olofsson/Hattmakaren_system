using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using iText.Kernel.Pdf;
using iText.Layout;
using Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for OrderBeskrivningPage.xaml
    /// </summary>
    public partial class OrderBeskrivningPage : Page
    {
        private Order order;
        private IOrderService _orderService;
        private IProduktService _produktService;
        public OrderBeskrivningPage(Order o)
        {
            InitializeComponent();
            var context = new DBcontext();
            _produktService = new ProduktService(new ProduktRepo(context), context);
            _orderService = new OrderService(new OrderRepo(context), context);
            order = o;
            DataContext = new OrderBeskrivningVM(order, _produktService);
        }

        private async void MarkeraSomKlar_Click(object sender, RoutedEventArgs e)
        {
            await _orderService.MarkeraFärdig(order.OrderID);
            var enOrder = await _orderService.GetOrder(order.OrderID);
            //order = _orderService.GetFullOrder(order.OrderID);
            if (order.Färdig)
            {
                order.Färdig = false;
            }
            else
            {
                order.Färdig = true;
            }

            DataContext = new OrderBeskrivningVM(order, _produktService);
            MessageBox.Show($"Ordern är nu markerad som: {enOrder.Färdig}");//Måste hämta nya värdet efter uppdateringen för att det ska synas rätt
        }

        private void LaddaNerPdfKnapp_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"Order_{order.OrderID}.pdf";

            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
            string pdfMapp = System.IO.Path.Combine(baseDirectory, "DAL", "OrderPdf");
            string orderPdfFullPath = System.IO.Path.Combine(pdfMapp, fileName);

            Directory.CreateDirectory(pdfMapp);

            Document dokument = new Document(new PdfDocument(new PdfWriter(orderPdfFullPath)));

            dokument.Add(new iText.Layout.Element.Paragraph($"Order: {order.OrderID}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Kund: {order.Kund.Namn}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Pris: {order.Pris} kr"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Rabatt: {order.Rabatt} %"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Datum: {order.Datum}"));
            //foreach (Produkt p in order.Produkter)
            //{
            //    dokument.Add(new iText.Layout.Element.Paragraph($" - {p.Namn} - {p.Pris}"));
            //}
            foreach (var rad in order.OrderRader)
            {
                dokument.Add(
                    new iText.Layout.Element.Paragraph(
                        $" - {rad.Produkt.Namn} - {rad.Produkt.Pris}"
                    )
                );
            }

            dokument.Add(new iText.Layout.Element.Paragraph($"Startare: {order.StartadAv.Namn}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Klar: {order.Färdig}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Specialbeställning: {order.IsSpecialbeställning}"));
            dokument.Close();
            MessageBox.Show($"En PDF har skapats på: {orderPdfFullPath}");
        }

        private async void OrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Detta anropas när användaren byter status i rullistan!
            // Se till att hämta din ViewModel
            if (this.DataContext is OrderBeskrivningVM vm)
            {
                // Här kan du anropa en metod i din Service för att spara den nya statusen!
                // t.ex: await _orderService.UppdateraOrderStatus(vm.OrdernsID, vm.OrderStatus);

                await _orderService.UppdateraOrderStatus(vm.OrdernsID, vm.OrderStatus);
            }
        }

        private async void BtnSkrivUtFraktsedel_Click(object sender, RoutedEventArgs e)
        {
            var fraktInfo = await _orderService.GetFraktForOrder(order.OrderID);

            try
            {
                string fileName = $"Order_{order.OrderID}.pdf";

                string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
                string pdfMapp = System.IO.Path.Combine(baseDirectory, "DAL", "Fraktsedlar");
                string orderPdfFullPath = System.IO.Path.Combine(pdfMapp, fileName);

                Directory.CreateDirectory(pdfMapp);

                Document dokument = new Document(new PdfDocument(new PdfWriter(orderPdfFullPath)));

                dokument.Add(new iText.Layout.Element.Paragraph($"Varukod: {order.Varukod}"));
                dokument.Add(new iText.Layout.Element.Paragraph($"Pris: {order.Pris} {(order.Kund.FöretagsKund ? "(25% Moms)" : "")}"));
                dokument.Add(new iText.Layout.Element.Paragraph($"Mottagare: {order.Kund.Namn}"));
                dokument.Add(new iText.Layout.Element.Paragraph($"Adress: {order.Kund.Land}, {order.Kund.Stad}, {order.Kund.Adress}"));

                foreach (var pRad in order.OrderRader)
                {
                    var produkt = await _produktService.GetProduktId(pRad.ProduktID);
                    string prodString = $" - {produkt.Namn} - {(pRad.Antal * produkt.Pris)} - {pRad.Antal} st";
                    dokument.Add(new iText.Layout.Element.Paragraph(prodString));
                }

                dokument.Add(new iText.Layout.Element.Paragraph($"Avsändare: Otto & Judith AB"));
                dokument.Add(new iText.Layout.Element.Paragraph($"Avsändare: Sweden, Örebro, Fabriksvägen 11B"));

                if (fraktInfo != null)
                {
                    dokument.Add(new iText.Layout.Element.Paragraph($"Transportör: {fraktInfo.Transportör}"));
                    dokument.Add(new iText.Layout.Element.Paragraph($"Sändningsnummer: {fraktInfo.Sändningsnummer}"));
                    dokument.Add(new iText.Layout.Element.Paragraph($"Kolli-ID: {fraktInfo.KolliId}"));
                }
                dokument.Close();

                MessageBox.Show($"En PDF har skapats på: {orderPdfFullPath}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
