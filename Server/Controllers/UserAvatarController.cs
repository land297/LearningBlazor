using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAvatarController : ControllerBase2<UserAvatar> {
        readonly IUserAvatarRepo _userAvatarRepo;
        readonly ILogger _logger;
        public UserAvatarController(ILogger<UserAvatarController> logger,
            IUserAvatarRepo repo3) : base(repo3) {
            _logger = logger;
            _userAvatarRepo = repo3;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(UserAvatar userAvatar) {
            return await Created(_userAvatarRepo.SaveInContext(userAvatar),"TODO: some uri that I will return");
        }

        [HttpPut("setactive/{id}")]
        public async Task<IActionResult> SetActive(int id) {
            _logger.LogInformation("setting active", id);
            return await Ok(_userAvatarRepo.SetActiveInContext(id));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            return await Ok(_userAvatarRepo.GetInContext(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            return await Ok(_userAvatarRepo.Delete(id));
        }
        [HttpPost("foruser")]
        public async Task<IActionResult> GetAllForUser(User user) {
            return await Ok(_userAvatarRepo.GetAllForUser_NoBlob(user));
        }
        [HttpGet("foruserActive")]
        public async Task<IActionResult> GetActiveForUser() {
            return await Ok(_userAvatarRepo.GetActiveInContext());
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            return await base.Ok(_userAvatarRepo.GetAllInContext());
        }
    }
}
