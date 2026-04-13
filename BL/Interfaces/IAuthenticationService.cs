using Models;

namespace BL.Interfaces
{
    public interface IAuthenticationService
    {
        Användare InloggadAnvändare { get; }
        bool Login(string username, string password);
    }
}
