using Learning.Server.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public class GenericRepo {
    }
    public interface IRepository<TEntity> where TEntity : class {
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context) {
            _dbSet = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes) {
            //IDbSet<TEntity> dbSet = Context.Set<TEntity>();

            IEnumerable<TEntity> query = null;
            foreach (var include in includes) {
                query = _dbSet.Include(include);
            }

            return query ?? _dbSet;
        }
        public IQueryable<TEntity> Include2(params Expression<Func<TEntity, object>>[] includes) {
            IIncludableQueryable<TEntity, object> query = null;

            if (includes.Length > 0) {
                query = _dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex) {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? _dbSet : (IQueryable<TEntity>)query;
        }

        public IEnumerable<TEntity> All() {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return _dbSet.Where(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate) {
            return _dbSet.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity) {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities) {
            _dbSet.AddRange(entities);
        }

        public void Remove(TEntity entity) {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities) {
            _dbSet.RemoveRange(entities);
        }
    }
}
