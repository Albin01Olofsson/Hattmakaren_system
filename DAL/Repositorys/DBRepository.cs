using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositorys
{
    public class DBRepository<T> : IRepository<T> where T : class
    {
        protected readonly DBcontext _context;
        protected readonly DbSet<T> _dbSet;

        public DBRepository(DBcontext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // READ: Hittar en specifik rad via ID
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // CREATE: Lägger till ett objekt i kön
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        // UPDATE: Informerar EF om att objektet har ändrats
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // DELETE: Tar bort objektet
        public void Delete(int id)
        {
            T existing = _dbSet.Find(id);
            if (existing != null)
            {
                _dbSet.Remove(existing);
            }
        }

        // COMMIT: Sparar allt till databasen
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
