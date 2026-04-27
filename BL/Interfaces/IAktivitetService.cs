using Models;

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
