using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
using Learning.Server.Service;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IForgottenPasswordRepo {
        Task<ForgottenPasswordRequest> NewRquest(string email);
        Task<bool> CheckValidRequst(string resetCode,string password);


    }

    public class ForgottenPasswordRepo : RepoBase2<ForgottenPasswordRequest>, IForgottenPasswordRepo {
        private readonly IUserRepo _userRepo;
        public ForgottenPasswordRepo(AppDbContext dbContext,IUserRepo userRepo) : base(dbContext) {
            _userRepo = userRepo;
        }

        public async Task<ForgottenPasswordRequest> NewRquest(string email) {
            var e = new ForgottenPasswordRequest()
            {
                Email = email,
                WhenAskedForNew = DateTime.UtcNow,
                Code = RandomString(5, true),
                
            };
            var r = await SaveAndGetEntity(e);
            return r.Item1;
        }
        public async Task<bool> CheckValidRequst(string resetCode,string password) {
            var entity = await DbSet.SingleOrDefaultAsync(x => x.Code == resetCode);
            if (entity == null || entity.Code == "used") {
                return false;
            }

            if (entity.WhenAskedForNew.AddMinutes(15) > DateTime.UtcNow && DateTime.UtcNow.AddMinutes(-15) < entity.WhenAskedForNew) {
                var user = await _userRepo.GetUser(entity.Email);
                CreatePassword.CreatePasswordHash(password, out byte[] hash, out byte[] salt);

                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                //ownerEntry.RefreshToken = GetRefreshToken();

                await _userRepo.UpdateUser(user);
                entity.Code = "used";
                 DbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private string RandomString(int size, bool lowerCase) {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++) {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
