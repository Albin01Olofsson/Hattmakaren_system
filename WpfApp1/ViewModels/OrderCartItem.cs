using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class OrderCartItem
    {
        public int ProduktID { get; set; }
        public string Namn { get; set; }
        public decimal Pris { get; set; }
        public int Antal { get; set; }
    }
}
