using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace Repository
{
    public class GenericRepositoryEF<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Get()
        {
            using (var context = new EquipmentContainer())
            {
                context.Set<TEntity>().Load();
                return context.Set<TEntity>().Local;
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new EquipmentContainer())
            {
                context.Set<TEntity>().AddOrUpdate(entity);
                context.SaveChanges();
            }
        }

        public void Add(TEntity entity)
        {
            Update(entity);
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<TEntity> branch)
        {
            using (var context = new EquipmentContainer())
            {
                foreach (var item in branch)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }


        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            //return Include(includeProperties).ToList();

            using (var context = new EquipmentContainer())
            {
                IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
            }
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var context = new EquipmentContainer())
            {
                IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
        }

        public TEntity FindById(int id)
        {
            using (var context = new EquipmentContainer())
            {
                return context.Set<TEntity>().Find(id);
            }
        }
    }
}
