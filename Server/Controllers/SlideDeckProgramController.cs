using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlideDeckProgramController : ControllerBase2<SlideDeckProgram> {
        private readonly ISlideDeckProgramRepo _slideDeckProgramRepo;
        private readonly IUserService _userService;
        private readonly IUserAvatarRepo _userAvatar;
        //private readonly IGetter<SlideDeckProgram> _getter;
        public SlideDeckProgramController(ISlideDeckProgramRepo slideDeckProgramRepo,
             IUserService userService, IUserAvatarRepo userAvatarRepo) :base (slideDeckProgramRepo, userService) {
            _slideDeckProgramRepo = slideDeckProgramRepo;
            //_getter = _slideDeckProgramRepo as IGetter<SlideDeckProgram>;
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Save(SlideDeckProgram slideDeckProgram) {
            return await CreatedIntUri3(() => _slideDeckProgramRepo.SaveAndGetId(slideDeckProgram),(id) => $"api/SlideDeckProgram/" + id);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsUser() {
            //TODO: return only introductions!
            return await TryOk(() => _slideDeckProgramRepo.GetAllAsUser());
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAllAsContentCreator() {
            return await TryOk(() => _slideDeckProgramRepo.GetAllAsContentCreator());
        }
        //TODO: refactor get methods
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            // TODO: need to check if user can get unpublished or not
            return await TryOk(async () => {
                var program = await _slideDeckProgramRepo.GetFirst(id);

                var activeAvatar = await _userAvatar.GetActiveInContext();

                if (program.AccessLevel == Shared.Models.Enums.AccessLevel.Premium &&
               (_userService.GetAccessLevel() != Shared.Models.Enums.UserRole.Premium
               || activeAvatar.PersonalProgramAccess.Count(x => x.SlideDeckProgramId == program.Id) == 0)) {
                    // does not have access. return dummy deck!
                    var dummy = await RepoBase.Get(10);
                    dummy.Title = program.Title;
                    dummy.Description = program.Description + Environment.NewLine + Environment.NewLine + dummy.Description;

                    return dummy;
                }
                return program;

            });
            //return await Get<SlideDeckProgram>(_slideDeckProgramRepo.GetFirst(id));
        }
        //[HttpGet("{slug}")]
        //public async Task<IActionResult> Get(string slug) {
        //    // TODO: need to check if user can get unpublished or not
        //    return await Get<SlideDeckProgram>(_slideDeckProgramRepo.GetFirst(slug));
        //}
        public async Task<IActionResult> Get<T>(Task<T> taskToGetFirst) {
            return await TryOk<T>(() => taskToGetFirst);
        }
    }
}
