using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public abstract class RepoBase {
        protected readonly AppDbContext _dbContext;
        public RepoBase(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
    }

    public interface IUserRepo {
        Task<sr<int>> AddUser(User user);
        Task<sr<bool>> UsersExists(User user);
        Task<User> GetUser(int id);
    }

    public class UserRepo : RepoBase, IUserRepo {
        //private readonly AppDbContext _dbContext;
        public UserRepo(AppDbContext dbContext) : base(dbContext) {
            //_dbContext = dbContext;
        }
        public async Task<sr<int>> AddUser(User user) {
            if (await UsersExits(user.Email)) {
                return sr<int>.Get("User already exists");
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return sr<int>.GetSuccess(user.Id);
        }
        public async Task<sr<bool>> UsersExists(User user) {
            return sr<bool>.Get(await UsersExits(user.Email));
        }

        public async Task<bool> UsersExits(string email) {
            return await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> UsersExits(int id) {
            return await _dbContext.Users.AnyAsync(x => x.Id == id);
        }
        private async Task<User> GetUser(string email) {
            return await _dbContext.Users.FirstOrDefaultAsync<User>(x => x.Email.ToLower() == email.ToLower());
        }
        public async Task<User> GetUser(int id) {
            return await _dbContext.Users.FirstOrDefaultAsync<User>(x => x.Id == id);
        }
    }
}
