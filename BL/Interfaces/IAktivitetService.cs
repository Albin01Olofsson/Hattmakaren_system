using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAktivitetService
    {
        Task LäggTillAktivitet(Aktivitet aktivitet);
        Task UpdateraAktivitet(Aktivitet aktivitet);
        Task<Aktivitet> HämtaAktivitetById(int id);
        Task<List<Aktivitet>> HämtaAllaAktiviteter();
        Task TaBortAktivitet(int id);
    }
}
