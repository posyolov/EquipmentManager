using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerM
{
    public class RepositoryProxy<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        IGenericRepository<TEntity> _genericRepository;

        public RepositoryProxy(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Add(TEntity entity)
        {
            _genericRepository.Add(entity);
        }

        public TEntity FindById(int id)
        {
            return _genericRepository.FindById(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return _genericRepository.Get();
        }

        public IEnumerable<TEntity> GetWithInclude(params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _genericRepository.GetWithInclude(includeProperties);
        }

        public void Remove(TEntity entity)
        {
            _genericRepository.Remove(entity);
        }

        public Exception RemoveRange(List<TEntity> entities)
        {
            return _genericRepository.RemoveRange(entities);
        }

        public Exception Update(TEntity entity)
        {
            return _genericRepository.Update(entity);
        }
    }
}
