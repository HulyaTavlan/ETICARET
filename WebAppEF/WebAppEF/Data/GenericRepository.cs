using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebAppEF.Data
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T entity);
        T ReadOne(object entityKey);
        IEnumerable<T> ReadAll(string includes = null);
        IEnumerable<T> ReadMany(Expression<Func<T, bool>> expression, string includes = null);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        void Dispose();
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _db;
        protected DbSet<T> _dbSet;

        public GenericRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<T> ReadAll(string includes = null)
        {
            IQueryable<T> set = _dbSet;

            if (includes != null)
            {
                var paths = includes.Split(',');
                foreach (var item in paths)
                {
                    set = set.Include(item);
                }
            }

            return set;
        }

        public IEnumerable<T> ReadMany(Expression<Func<T, bool>> expression, string includes = null)
        {
            IQueryable<T> set = _dbSet.Where(expression);

            if (includes != null)
            {
                var paths = includes.Split(',');
                foreach (var item in paths)
                {
                    set = set.Include(item);
                }
            }

            return set;
        }

        public T ReadOne(object entityKey)
        {
            return _dbSet.Find(entityKey);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}