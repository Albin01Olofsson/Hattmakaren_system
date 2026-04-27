using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IProduktMaterialService
    {
        Task SkapaProduktOchDraLager(Produkt produkt, List<ProduktMaterial> material);
    }
}
