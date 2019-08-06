using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        ObservableCollection<TEntity> Get();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
