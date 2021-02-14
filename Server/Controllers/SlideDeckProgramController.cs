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
        //private readonly IGetter<SlideDeckProgram> _getter;
        public SlideDeckProgramController(ISlideDeckProgramRepo slideDeckProgramRepo,
             IUserService us) :base (slideDeckProgramRepo,us){
            _slideDeckProgramRepo = slideDeckProgramRepo;
            //_getter = _slideDeckProgramRepo as IGetter<SlideDeckProgram>;
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Save(SlideDeckProgram slideDeckProgram) {
            //TODO: check if user is authorized
            return await CreatedIntUri(_slideDeckProgramRepo.SaveAndGetId(slideDeckProgram),$"api/SlideDeckProgram/",slideDeckProgram);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsUser() {
            return await Ok(_slideDeckProgramRepo.GetAllAsUser());
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAllAsContentCreator() {
            return await Ok(_slideDeckProgramRepo.GetAllAsContentCreator());
        }
        //TODO: refactor get methods
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            // TODO: need to check if user can get unpublished or not
            return await Get<SlideDeckProgram>(_slideDeckProgramRepo.GetFirst(id));
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug) {
            // TODO: need to check if user can get unpublished or not
            return await Get<SlideDeckProgram>(_slideDeckProgramRepo.GetFirst(slug));
        }
        public async Task<IActionResult> Get<T>(Task<T> taskToGetFirst) {
            return await Ok<T>(taskToGetFirst);
        }
    }
}
