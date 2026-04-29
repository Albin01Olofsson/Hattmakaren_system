using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1;
using WpfApp1.Views1;
public partial class BestallningarListaViewModel : ObservableObject
{
    private readonly DBcontext _context;

    public BestallningarListaViewModel()
    {
        _context = new DBcontext();
        LaddaBeställningar();
    }

    [ObservableProperty]
    private ObservableCollection<MaterialBeställning> bestallningar;
    public List<string> FilterOptions { get; } = new()
    {
        "Alla",
        "Beställda",
        "Levererade"
    };
    [ObservableProperty]
    private string selectedFilter = "Alla";
    partial void OnSelectedFilterChanged(string value)
    {
        LaddaBeställningar();
    }
    private void LaddaBeställningar()
    {
        var query = _context.MaterialBeställningar
            .Include(b => b.Rader)
            .ThenInclude(r => r.Material)
            .AsQueryable();

        switch (SelectedFilter)
        {
            case "Beställda":
                query = query.Where(b => !b.Levererad);
                break;

            case "Levererade":
                query = query.Where(b => b.Levererad);
                break;
        }

        Bestallningar = new ObservableCollection<MaterialBeställning>(query.ToList());
    }

    [RelayCommand]
    private void GoBack()
    {
        var window = (MainWindow)Application.Current.MainWindow;
        var mainPage = window.MainFrame.Content as Mainpage;

        mainPage?.GetFrame().Navigate(new BestallningarPage());
    }

    [RelayCommand]
    private void UpdateLevererad(MaterialBeställning bestallning)
    {
        var item = _context.MaterialBeställningar
            .FirstOrDefault(b => b.MaterialBeställningID == bestallning.MaterialBeställningID);

        if (item != null)
        {
            item.Levererad = bestallning.Levererad;
            _context.SaveChanges();
        }
    }
}