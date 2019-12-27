using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    /// <summary>
    /// Интерфейс обобщённого репозитория
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Запрос всех сущностей заданного типа
        /// </summary>
        /// <returns>IEnumerable<TEntity></returns>
        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);       //??? добавить результат выполнения ???
        Exception Update(TEntity entity);      //??? добавить результат выполнения ???
        void Remove(TEntity entity);      //??? добавить результат выполнения ???
        Exception RemoveRange(List<TEntity> entities);      //??? добавить результат выполнения ???
        TEntity FindById(int id);
    }
}
