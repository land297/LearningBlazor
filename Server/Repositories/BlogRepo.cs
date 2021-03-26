using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public class BlogRepo : RepoBase2<BlogPost> {
        public BlogRepo(AppDbContext dbContext) : base(dbContext) {
        }
    }
}
