using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class AnvändarePageViewModel
    {
        private readonly IAnvändarService _användarService;

        public AddAnvändareViewModel AddAnvändareVM { get; set; }
        public AnvändareViewModel ListaVM { get; set; }

        public AnvändarePageViewModel(IAnvändarService användarService)
        {
            _användarService = användarService;
            AddAnvändareVM = new AddAnvändareViewModel();
            ListaVM = new AnvändareViewModel(_användarService);
            AddAnvändareVM.AnvändareAdded += (användare) =>
            {
                användare.IsActive = true;
                _användarService.LäggTillAnvändare(användare);
                ListaVM.AnvändareLista.Add(användare);
            };
        }

    }
}
