using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IUserRepo {
        Task<sr<int>> AddUser(UserRegistration user);
        Task<sr<bool>> UsersExists(User user);
        Task<sr<User>> Get(int id);
        Task<User> GetUser(string email);
        Task<sr<IList<User>>> GetAll();
    }

    public class UserRepo : RepoBase2<User>, IUserRepo,IRepoBase3<User,UserRegistration> {
        public UserRepo(AppDbContext dbContext) : base(dbContext) {
     
        }
        public async Task<sr<int>> Save(UserRegistration dto) {
            return await AddUser(dto);
        }
        public async Task<sr<int>> AddUser(UserRegistration userRegistration) {
            if (await UsersExits(userRegistration.Email)) {
                return sr<int>.Get("User already exists");
            }
            CreatePassword.CreatePasswordHash(userRegistration.Password, out byte[] hash, out byte[] salt);
            var dbUser = new User();
            dbUser.PasswordHash = hash;
            dbUser.PasswordSalt = salt;
            dbUser.Email = userRegistration.Email;
            dbUser.Bio = userRegistration.Bio;
            dbUser.Username = userRegistration.FirstName;

            return  await base.Save(dbUser);
            //await _dbContext.Users.AddAsync(dbUser);
            //await _dbContext.SaveChangesAsync();

            //return sr<int>.GetSuccess(dbUser.Id);
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
        public async Task<User> GetUser(string email) {
            return await _dbContext.Users.FirstOrDefaultAsync<User>(x => x.Email.ToLower() == email.ToLower());
        }
        //public async Task<User> GetUser(int id) {
        //    return await _dbContext.Users.FirstOrDefaultAsync<User>(x => x.Id == id);
        //}
    }
}
