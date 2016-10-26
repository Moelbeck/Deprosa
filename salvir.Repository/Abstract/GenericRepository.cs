using deprosa.Repository.DatabaseContext;
using deprosa.Model.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace deprosa.Repository.Abstract
{
    public class GenericRepository<T>  where T : Entity 
    {
        private BzaleDatabaseContext _entities;
        public GenericRepository(BzaleDatabaseContext context)
        {
            _entities = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate).AsNoTracking();
            return query;
        }
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).AsNoTracking().FirstOrDefault(); 
        }

        public virtual T Add(T entity)
        {
            entity.Created = DateTime.Now;
            _entities.SaveChanges();
            return _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            entity.Deleted = DateTime.Now;
            //_entities.Set<T>().Remove(entity);
            _entities.SaveChanges();

        }
        public virtual void Delete(int entityid)
        {
            var entity = GetSingle(e => e.ID == entityid);
            entity.Deleted = DateTime.Now;
            //_entities.Set<T>().Remove(entity);
            _entities.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            entity.Updated = DateTime.Now;
            entity.Deleted = null;
            _entities.Set<T>().Attach(entity);
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        protected virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
