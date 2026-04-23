using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WpfApp1.ViewModels
{
    public partial class SeFörfrågningVM : ObservableObject
    {
        [ObservableProperty]
        private String avsändare = string.Empty;

        public SeFörfrågningVM(Mail m)
        {
            Avsändare = m.Avsändare;
        }
    }
}
