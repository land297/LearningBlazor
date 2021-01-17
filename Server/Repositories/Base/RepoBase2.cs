using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learning.Server.Repositories.Base {
    public interface IRepoBase2<T> where T : IdEntity<T> {
        DbSet<T> DbSet { get; }
        Task<sr<T>> Get(Expression<Func<T, bool>> predicate);
        Task<sr<IList<T>>> Get(Task<List<T>> task);
        Task<sr<T>> Get(Task<T> task);
        Task<sr<IList<T>>> GetAll();
        Task<sr<T>> Save(T entity);
        Task<sr<T>> Remove(Task<T> task);
    }
    public interface IRepoBase3<T,K> where T : IdEntity<T> {
        DbSet<T> DbSet { get; }
        Task<sr<T>> Get(Expression<Func<T, bool>> predicate);
        Task<sr<IList<T>>> Get(Task<List<T>> task);
        Task<sr<T>> Get(Task<T> task);
        Task<sr<T>> Get(int id);
        Task<sr<IList<T>>> GetAll();
        Task<sr<T>> Save(T entity);
        Task<sr<int>> Save(K dto);
        Task<sr<T>> Remove(Task<T> task);
    }

    public abstract class RepoBase2<T> : IRepoBase2<T> where T : IdEntity<T> {
        protected readonly AppDbContext _dbContext;
        public DbSet<T> DbSet { get; private set; }
        public RepoBase2(AppDbContext dbContext) {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }
        public async Task<sr<T>> Remove(Task<T> task) {
            try {
                var result = await task;
                DbSet.Remove(result);
                await _dbContext.SaveChangesAsync();
                return sr<T>.GetSuccess(result);
            } catch (Exception e) {
                return sr<T>.Get(e);
            }
        }
        public async Task<sr<T>> Remove(T entity) {
            try {
                DbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return sr<T>.GetSuccess(entity);
            } catch (Exception e) {
                return sr<T>.Get(e);
            }
        }
        public async Task<sr<T>> Get(Task<T> task) {
            try {
                var result = await task;
                if (result == null) {
                    return sr<T>.Get("Null");
                }
                return sr<T>.GetSuccess(result);
            } catch {
                return sr<T>.Get();
            }
        }
        public async Task<sr<T>> Get(int id) {
            try {
                var result = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null) {
                    return sr<T>.Get("Null");
                }
                return sr<T>.GetSuccess(result);
            } catch {
                return sr<T>.Get();
            }
        }
        public async Task<sr<IList<T>>> Get(Task<List<T>> task) {
            try {
                var result = await task;
                return sr<IList<T>>.GetSuccess(result);
            } catch {
                return sr<IList<T>>.Get();
            }
        }

        public async Task<sr<T>> Get(Expression<Func<T, bool>> predicate) {
            try {
                var result = await DbSet.SingleOrDefaultAsync(predicate);
                return sr<T>.GetSuccess(result);
            } catch {
                return sr<T>.Get();
            }
        }
        public async Task<sr<IList<T>>> GetAll() {
            try {
                var result = await DbSet.ToListAsync();
                return sr<IList<T>>.GetSuccess(result);
            } catch {
                return sr<IList<T>>.Get();
            }
        }
        public virtual async Task<sr<T>> Save(T entity) {
            try {
                if (entity.Id != default(int)) {
                    var result = await Get(x => x.Id == entity.Id);
                    if (result.Success) {
                        result.Data.CopyValues(result.Data, ref entity);
                    }
                } else {
                    await DbSet.AddAsync(entity);
                }
                await _dbContext.SaveChangesAsync();
                return sr<T>.GetSuccess(entity);
            } catch (Exception e) {
                return sr<T>.Get(e);
            }
        }
    }
}
