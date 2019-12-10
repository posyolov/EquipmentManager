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
    /// <summary>
    /// Обобщённый репозиторий Entity Framework
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepositoryEF<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext _context;

        public GenericRepositoryEF(DbContext context)
        {
            context.Set<TEntity>().Count();


            try
            {
                context.Set<TEntity>().Count();
                //var kuku = context.Set(typeof(TEntity));
            }
            catch
            {
                throw new NotImplementedException();
            }

            _context = context;
        }

        /// <summary>
        /// Запрос всех сущностей заданного типа
        /// </summary>
        /// <returns>IEnumerable<TEntity></returns>
        public IEnumerable<TEntity> Get()
        {
            //using (var context = new EquipmentContainer())
            {
                _context.Set<TEntity>().Load();
                return _context.Set<TEntity>().Local;
            }
        }

        public void Update(TEntity entity)
        {
            //using (var context = new EquipmentContainer())
            {
                _context.Set<TEntity>().AddOrUpdate(entity);
                _context.SaveChanges();
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

        public Exception RemoveRange(List<TEntity> branch)
        {
            //using (var context = new EquipmentContainer())
            {
                foreach (var item in branch)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                try
                {
                    _context.SaveChanges();
                    return null;
                }
                catch(Exception ex)
                {
                    foreach (var item in branch)
                    {
                        _context.Entry(item).State = EntityState.Unchanged;
                    }
                    return ex;
                }
            }
        }


        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            //return Include(includeProperties).ToList();

            //using (var context = new EquipmentContainer())
            {
                IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
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
            //using (var context = new EquipmentContainer())
            {
                IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
        }

        public TEntity FindById(int id)
        {
            //using (var context = new EquipmentContainer())
            {
                return _context.Set<TEntity>().Find(id);
            }
        }
    }
}
