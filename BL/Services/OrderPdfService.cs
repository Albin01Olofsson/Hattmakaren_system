using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace BL.Services
{
    public class OrderPdfService
    {
        public void CreatePdf()
        {

            Document dokument = new Document(new PdfDocument(new PdfWriter("Order.pdf")));
            dokument.Add(new Paragraph("Hejsan! Lyckades."));
            dokument.Close();
        }
    }
}
