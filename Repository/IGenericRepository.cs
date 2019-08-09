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
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);       //??? добавить результат выполнения ???
        void Update(TEntity entity);      //??? добавить результат выполнения ???
        void Remove(TEntity entity);      //??? добавить результат выполнения ???
        void RemoveRange(List<TEntity> entities);      //??? добавить результат выполнения ???
    }
}
