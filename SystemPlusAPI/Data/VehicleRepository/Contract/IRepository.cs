using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using SystemPlusAPI.Models;

namespace SystemPlusAPI.Data.VehicleRepository.Contract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetSingleOrDefault(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void Save();
    }
}
