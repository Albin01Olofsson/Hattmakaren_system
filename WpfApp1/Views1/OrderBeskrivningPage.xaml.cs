using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using iText.Kernel.Pdf;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.IO.Image;
using iText.Layout.Element;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for OrderBeskrivningPage.xaml
    /// </summary>
    public partial class OrderBeskrivningPage : Page
    {
        private Order order;
        public OrderBeskrivningPage(Order o)
        {
            InitializeComponent();
            IProduktService produktService = new ProduktService(new ProduktRepo(new DBcontext()));
            DataContext = new OrderBeskrivningVM(o, produktService);
            order = o;
;       }

        private void LaddaNerPdfKnapp_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"Order_{order.OrderID}.pdf";

            string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;
            string pdfMapp = System.IO.Path.Combine(baseDirectory, "DAL", "OrderPdf");
            string orderPdfFullPath = System.IO.Path.Combine(pdfMapp, fileName);

            Directory.CreateDirectory(pdfMapp);

            Document dokument = new Document(new PdfDocument(new PdfWriter(orderPdfFullPath)));

            dokument.Add(new iText.Layout.Element.Paragraph($"Order: {order.OrderID}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Kund: {order.Kund}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Pris: {order.Pris} kr"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Rabatt: {order.Rabatt} %"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Datum: {order.Datum}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Startare: {order.StartadAv.Namn}"));
            dokument.Add(new iText.Layout.Element.Paragraph($"Specialbeställning: {order.IsSpecialbeställning}"));
            dokument.Close();
            MessageBox.Show($"En PDF har skapats på: {orderPdfFullPath}");
        }
    }
}
