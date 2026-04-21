using BL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using DAL.Repositorys;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp1.Views1;
using System.Windows;
using WpfApp1;
using Microsoft.EntityFrameworkCore;
public partial class BestallningarListaViewModel : ObservableObject
{
    private readonly DBcontext _context;

    public BestallningarListaViewModel()
    {
        _context = new DBcontext();

        var list = _context.MaterialBeställningar
            .Include(b => b.Rader)
            .ThenInclude(r => r.Material)
            .ToList();

        Bestallningar = new ObservableCollection<MaterialBeställning>(list);
    }

    [ObservableProperty]
    private ObservableCollection<MaterialBeställning> bestallningar;

    [RelayCommand]
    private void GoBack()
    {
        var mainWindow = (MainWindow)Application.Current.MainWindow;
        mainWindow.MainFrame.Navigate(new BestallningarPage());
    }
}