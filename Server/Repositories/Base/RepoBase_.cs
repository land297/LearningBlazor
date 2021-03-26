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
        Task<List<Tentiy>> GetAll();
        Task<Tuple<int, string>> SaveAndGetId(Tentiy entity);
        Task<Tuple<int, string>> SaveDtoAndGetId(object dto);
        Task<Tuple<Tentiy, string>> SaveAndGetEntity(Tentiy entity);
        Task<Tuple<Tentiy, string>> SaveDtoAndGetEntity(object dto);
        //Task<Tentiy> Remove(Task<Tentiy> task);
        Task<Tentiy> Remove(Tentiy entity);
        Task<Tentiy> Remove(int id);
    }

    public class RepoValidation {
        public bool Valid { get; set;}
        public string Message { get; set; }
        public RepoValidation(bool valid, string message) {
            Valid = valid;
            Message = message;
        }
        public static RepoValidation GetValid() {
            return new RepoValidation(true, string.Empty);
        }
        public static RepoValidation GetInValid(string message) {
            return new RepoValidation(false, message);
        }
    }
    public abstract class RepoBase2<T> : IRepoBase3<T> where T : IdEntity<T> {
        protected readonly AppDbContext _dbContext;
        public DbSet<T> DbSet { get; private set; }
        public RepoBase2(AppDbContext dbContext) {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }
        public Dictionary<Type, Func<object, T>> DtoToEntityTransforms = new Dictionary<Type, Func<object, T>>();
        public Func<T, Task<RepoValidation>> ValidateEntityStateBeforeSave;

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
        public virtual Task<T> Get(int id) {
            return DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<T> GetFirst(Expression<Func<T, bool>> predicate) {
            return DbSet.FirstOrDefaultAsync(predicate);
        }
        public Task<List<T>> GetAll() {
            return DbSet.ToListAsync();
        }
        public virtual async Task<Tuple<int, string>> SaveDtoAndGetId(object obj) {
            if (DtoToEntityTransforms.ContainsKey(obj.GetType())) {
                var entity = DtoToEntityTransforms[obj.GetType()](obj);
                if (entity != null) {
                    return await SaveAndGetId(entity);
                }
            }
            return default;
        }
        public virtual async Task<Tuple<int, string>> SaveAndGetId(T entity) {
            if (ValidateEntityStateBeforeSave != null) {
                var state = await ValidateEntityStateBeforeSave.Invoke(entity);
                if (!state.Valid) {
                    return Tuple.Create<int, string>(default, state.Message);
                }
            }
            if (entity.Id != default(int)) {
                var result = await GetFirst(x => x.Id == entity.Id);
                result.CopyValues(result, ref entity);
            } else {
                await DbSet.AddAsync(entity);
            }
            await _dbContext.SaveChangesAsync();
            return Tuple.Create<int, string>(entity.Id,string.Empty);
        }
        public virtual async Task<Tuple<T,string>> SaveDtoAndGetEntity(object obj) {
            if (DtoToEntityTransforms.ContainsKey(obj.GetType())) {
                var entity = DtoToEntityTransforms[obj.GetType()](obj);
                if (entity != null) {
                    return await SaveAndGetEntity(entity);
                }
            } 
            return default;
        }
        public virtual async Task<Tuple<T,string>> SaveAndGetEntity(T entity) {
            try {
                if (ValidateEntityStateBeforeSave != null) {
                    var state = await ValidateEntityStateBeforeSave.Invoke(entity);
                    if (!state.Valid) {
                        return Tuple.Create<T,string>(null, state.Message);
                    }
                }
             
                if (entity.Id != default(int)) {
                    var result = await GetFirst(x => x.Id == entity.Id);
                    result.CopyValues(result, ref entity);
                } else {
                    await DbSet.AddAsync(entity);
                }
                await _dbContext.SaveChangesAsync();
                return Tuple.Create(entity, string.Empty);
            } catch (Exception ex) {
                // TODO: logger logg ex ?
                throw;
            }
        }
        
    }
}
