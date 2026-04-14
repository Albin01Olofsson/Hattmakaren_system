using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BL.Interfaces
{
    public interface IAuthenticationService
    {
        Användare Login(string username, string password);
    }
}
