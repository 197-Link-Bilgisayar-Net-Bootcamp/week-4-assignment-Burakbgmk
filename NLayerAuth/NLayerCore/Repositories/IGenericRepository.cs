using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerCore.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        void Delete(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
