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
    public interface IRepoBase3<Tentiy> where Tentiy : IdEntity<Tentiy> {
        DbSet<Tentiy> DbSet { get; }
        Task<Tentiy> GetFirst(Expression<Func<Tentiy, bool>> predicate);
        //Task<IList<Tentiy>> Get(Task<List<Tentiy>> task);
        //Task<Tentiy> Get(Task<Tentiy> task);
        Task<Tentiy> Get(int id);
        Task<IList<Tentiy>> GetAll();
        Task<int> Save(Tentiy entity);
        Task<int> Save(object dto);
        Task<Tentiy> SaveAndGetEntity(Tentiy entity);
        Task<Tentiy> SaveAndGetEntity(object dto);
        //Task<Tentiy> Remove(Task<Tentiy> task);
        Task<Tentiy> Remove(Tentiy entity);
        Task<Tentiy> Remove(int id);
    }

    public abstract class RepoBase2<T> : IRepoBase3<T> where T : IdEntity<T> {
        protected readonly AppDbContext _dbContext;
        public DbSet<T> DbSet { get; private set; }
        public RepoBase2(AppDbContext dbContext) {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }
        public async Task<T> Remove(Task<T> task) {
            var result = await task;
            DbSet.Remove(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }
        public async Task<T> Remove(T entity) {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Remove(int id) {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        //public async Task<T> Get(Task<T> task) {
        //    var result = await task;
        //    return result;
        //}
        public async Task<T> Get(int id) {
            var result = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        //public async Task<IList<T>> Get(Task<List<T>> task) {
        //    return await task;
        //}

        public async Task<T> GetFirst(Expression<Func<T, bool>> predicate) {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<IList<T>> GetAll() {
            return await DbSet.ToListAsync();
        }
        public abstract Task<int> Save(object obj);
        public virtual async Task<int> Save(T entity) {
            if (entity.Id != default(int)) {
                var result = await GetFirst(x => x.Id == entity.Id);
                result.CopyValues(result, ref entity);
            } else {
                await DbSet.AddAsync(entity);
            }
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public abstract Task<T> SaveAndGetEntity(object obj);
        public virtual async Task<T> SaveAndGetEntity(T entity) {
            try {   
                if (entity.Id != default(int)) {
                    var result = await GetFirst(x => x.Id == entity.Id);
                    result.CopyValues(result, ref entity);
                } else {
                    await DbSet.AddAsync(entity);
                }
                await _dbContext.SaveChangesAsync();
                return entity;
            } catch (Exception ex) {
                // TODO: logger logg ex
                throw;
            }
        }
        
    }
}
