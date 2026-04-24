using Models;

namespace BL.Interfaces
{
    public interface IProduktService
    {
        Task<List<Produkt>> GetProdukt();
        Task<List<Produkt>> GetProdukter(); //Med Include
        Task<Produkt> GetProduktId(int id);
        Task AddProdukt(Produkt p, List<int> materialIdn);
        Task AddSpecialBeställning(SpecialBeställning sb, List<int> materialIdn);
        Task UpdateProdukt(Produkt p);
        Task DeleteProdukt(int id);
        Task SaveProdukt();
        Task<Produkt> HämtaFörstaLedigaProdukt(int artikelId);
    }
}
