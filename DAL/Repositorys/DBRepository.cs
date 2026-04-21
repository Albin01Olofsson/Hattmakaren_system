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

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        // READ: Hittar en specifik rad via ID
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // CREATE: Lägger till ett objekt i kön
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // UPDATE: Informerar EF om att objektet har ändrats
        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // DELETE: Tar bort objektet
        public async Task Delete(int id)
        {
            T existing = await _dbSet.FindAsync(id);
            if (existing != null)
            {
                _dbSet.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        // COMMIT: Sparar allt till databasen
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
