using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProps = null,
            bool isTracking = true
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filter = null,
            string includeProps = null,
            bool isTracking = true
            );

        Task Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
