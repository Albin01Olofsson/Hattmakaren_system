using CommunityToolkit.Mvvm.ComponentModel;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.ObjectModel;
using System.Linq;

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
}