using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public partial class SelectableAnvändare : ObservableObject
    {
        public Användare User { get; set; }

        [ObservableProperty]
        private bool isSelected;
    }
}
