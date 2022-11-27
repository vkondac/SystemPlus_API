using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SystemPlusAPI.Data.VehicleRepository.Contract;

namespace SystemPlusAPI.Data.VehicleRepository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
      
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            return query.Where(filter).SingleOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }
    }
}
