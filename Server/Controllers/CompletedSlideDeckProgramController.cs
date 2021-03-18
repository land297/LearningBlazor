using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedSlideDeckProgramController : ControllerBase2<CompletedSlideDeckProgram> {
        private readonly ICompletedSlideDeckProgramRepo _completedProgramRepo;

        public CompletedSlideDeckProgramController(ICompletedSlideDeckProgramRepo completedProgramRepo,
             IUserService us) : base (completedProgramRepo,us) {
            _completedProgramRepo = completedProgramRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Post(CompletedSlideDeckProgram completedProgram) {
            return await CreatedIntUri3(() =>_completedProgramRepo.Save(completedProgram),
                (entity) => { return $"api/user/{entity.Id}"; });
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            return await TryOk(() => _completedProgramRepo.GetAll());
            
        }
        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetAll(int id) {
            return await TryOk(() => _completedProgramRepo.GetAllForUserAvatar(id));
        }
        [HttpGet("all/shared/{id}")]
        public async Task<IActionResult> GetShared(int id) {
            return await TryOk(() => _completedProgramRepo.GetShared(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("any/{id}")]
        public async Task<IActionResult> GetAny(int id) {
            return await TryOk(() => _completedProgramRepo.GetAny(id));
        }
    }
}
