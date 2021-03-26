using Learning.Server.Controllers.Base;
using Learning.Server.Repositories.Base;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase2<BlogPost> {
        public BlogController(IRepoBase3<BlogPost> blogRepo) {
            _blogRepo = blogRepo;
        }

        private IRepoBase3<BlogPost> _blogRepo;

        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Post(BlogPost blogpost) {
            //TODO: check if user is authorized
            return await CreatedIntUri3<int>(() => _blogRepo.SaveAndGetId(blogpost), (id) => "api/blog/" + id);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublicPublished() {
            return await TryOk(() => _blogRepo.DbSet.Where(p => p.Published != DateTime.MinValue && !p.IsDeleted && p.AccessLevel != Shared.Models.Enums.AccessLevel.Premium).ToListAsync());
            //return await TryOk(() => _blogRepo.DbSet.ToListAsync());
        }
    }
}
