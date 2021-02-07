﻿using Learning.Server.DbContext;
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
        Task<int> AddUser(UserRegistration user);
        Task<bool> UsersExists(User user);
        Task<User> Get(int id);
        Task<User> GetUser(string email);
        Task<IList<User>> GetAll();
    }

    public class UserRepo : RepoBase2<User>, IUserRepo {
        public UserRepo(AppDbContext dbContext) : base(dbContext) {
     
        }
        public override Task<int> Save(object obj) {
            var dto = obj as UserRegistration;
            if (dto != null) {
                return AddUser(dto);
            } else {
                return null;
            }
        }
        public override Task<User> SaveAndGetEntity(object obj) {
            throw new NotImplementedException();
        }
        public async Task<int> AddUser(UserRegistration userRegistration) {
            if (await UsersExits(userRegistration.Email)) {
                throw new Exception("Email already in use");
            }
            CreatePassword.CreatePasswordHash(userRegistration.Password, out byte[] hash, out byte[] salt);
            var dbUser = new User();
            dbUser.PasswordHash = hash;
            dbUser.PasswordSalt = salt;
            dbUser.Email = userRegistration.Email;
            dbUser.Bio = userRegistration.Bio;
            dbUser.Username = userRegistration.FirstName;

            return  await base.Save(dbUser);
        }
        public async Task<bool> UsersExists(User user) {
            return await UsersExits(user.Email);
        }

        public Task<bool> UsersExits(string email) {
            return DbSet.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        public Task<bool> UsersExits(int id) {
            return DbSet.AnyAsync(x => x.Id == id);
        }
        public async Task<User> GetUser(string email) {
            var response = await GetFirst(x => x.Email.ToLower() == email.ToLower());
            return response;
        }
    }
}
