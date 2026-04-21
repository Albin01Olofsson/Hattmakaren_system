using Models;

namespace BL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Användare> Login(string email, string lösenord);
    }
}
