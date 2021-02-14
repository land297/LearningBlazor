using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessSlideDeckProgramController : ControllerBase2<UserAccessSlideDeckProgram> {
        private readonly IUserAccessSlideDeckProgramRepo _userAccessSlideDeckProgramRepo;
        readonly IUserRepo _userRepo;
        public UserAccessSlideDeckProgramController(IUserAccessSlideDeckProgramRepo userAccessSlideDeckProgramRepo, 
            IUserRepo userRepo,
            IUserService us) : base (userAccessSlideDeckProgramRepo,us){
            _userAccessSlideDeckProgramRepo = userAccessSlideDeckProgramRepo;
            _userRepo = userRepo;
        }

        //TODO: refactor get methods
        [HttpGet("userAvatar/{id:int}")]
        //public async Task<IActionResult> GetViaUserAvatar([FromBody] UserAvatar ua) {
        public async Task<IActionResult> GetViaUserAvatar(int id) {
            // TODO: need to check if user can get unpublished or not
            
            return await Ok(_userAccessSlideDeckProgramRepo.Get(new UserAvatar { Id = id }));
        
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAccessWithId(int id) {
            return await Ok(RepoBase.Remove(id));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessWithId(int id) {
            return await Ok(_userAccessSlideDeckProgramRepo.GetIncluded(id));
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetViaUser(User u) {
            // TODO: need to check if user can get unpublished or not
            var user = await _userRepo.GetUser(u.Email);
            if (user == null) {
                return BadRequest("No user");
            }
            return await Ok(_userAccessSlideDeckProgramRepo.Get(user));
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserAccessSlideDeckProgram userAccess) {
            // TODO: need to check if user can get unpublished or not

            return await CreatedIntUri3(() => _userAccessSlideDeckProgramRepo.SaveAndGetEntity(userAccess),(x) => "api/UserAccessSlideDeckProgram/" + x.Id);
        }
    }
}
