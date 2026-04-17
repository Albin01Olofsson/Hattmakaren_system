using BL.Interfaces;
using DAL;
using DAL.Intefaces;
using DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BL.Services
{
    public class MaterialBeställningService : IMaterialBeställningService
    {
        private readonly DBcontext _context;

        public MaterialBeställningService(DBcontext context)
        {
            _context = context;
        }

        public void SkapaBestallning(Material material, int antal, int användarId)
        {
            var beställning = new MaterialBeställning
            {
                StartadAvID = användarId,
                /*MaterialLista = new List<Material> { material }*/
                TotalPris = material.Pris * antal,
                Antal=antal
            };

            _context.MaterialBeställningar.Add(beställning);
            _context.SaveChanges();
            var all = _context.MaterialBeställningar.ToList();

            System.Diagnostics.Debug.WriteLine("ANTAL RADER I DB: " + all.Count);

            foreach (var b in all)
            {
                System.Diagnostics.Debug.WriteLine($"ID: {b.MaterialBeställningID}, Antal: {b.Antal}");
            }
            System.Diagnostics.Debug.WriteLine("DB NAME: " + _context.Database.GetDbConnection().Database);
        }
    }
}
    

