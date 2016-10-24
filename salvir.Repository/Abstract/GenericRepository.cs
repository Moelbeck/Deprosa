using depross.Repository.DatabaseContext;
using depross.Model.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace depross.Repository.Abstract
{
    public abstract class GenericRepository<T>  where T : Entity 
    {
        private BzaleDatabaseContext _entities;
        public GenericRepository(BzaleDatabaseContext context)
        {
            _entities = context;
        }

        protected IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate).AsNoTracking();
            return query;
        }
        protected T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).AsNoTracking().FirstOrDefault(); 
        }

        protected virtual T Add(T entity)
        {
            entity.Created = DateTime.Now;
            return _entities.Set<T>().Add(entity);
        }

        protected virtual void Delete(T entity)
        {
            entity.Deleted = DateTime.Now;
            _entities.Set<T>().Remove(entity);
        }
        public virtual void Delete(int entityid)
        {
            var entity = GetSingle(e => e.ID == entityid);
            entity.Deleted = DateTime.Now;
            _entities.Set<T>().Remove(entity);
        }

        protected virtual void Edit(T entity)
        {
            entity.Updated = DateTime.Now;
            entity.Deleted = null;
            _entities.Set<T>().Attach(entity);
        }

        protected virtual void Save()
        {
            _entities.SaveChanges();
        }

        protected virtual void SaveAsync()
        {
            _entities.SaveChangesAsync();
        }
    }
}
