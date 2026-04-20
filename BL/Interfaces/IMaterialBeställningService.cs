using Models;

namespace BL.Interfaces
{
    public interface IMaterialBeställningService
    {
        Task SkapaBestallning(MaterialBeställning bestallning);
    }
}
