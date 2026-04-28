using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IFraktjaktService
    {
        Task<List<FraktAlternativ>> HämtaFraktAlternativ(string land);
        Task<Frakt> BokaFrakt(int orderId, FraktAlternativ valtAlternativ);
        Task<List<SpårningsPunkt>> HämtaHistorik(string sändningsnummer);
    }
}
