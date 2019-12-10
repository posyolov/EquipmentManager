using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Обобщённый репозиторий Access
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepositoryAccess<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        StockContext _context;

        public GenericRepositoryAccess(StockContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get()
        {
            return _context.Load<TEntity>();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Exception RemoveRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
