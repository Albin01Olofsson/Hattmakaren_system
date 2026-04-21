using Models;

namespace BL.Interfaces
{
    public interface IMaterialService
    {
        Task<List<Material>> GetMaterialLista();
    }
}
