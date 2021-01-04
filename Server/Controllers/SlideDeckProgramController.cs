using Learning.Server.Repositories;
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
    public class SlideDeckProgramController : ControllerBase {
        private readonly ISlideDeckProgramRepo _slideDeckProgramRepo;
        //private readonly IGetter<SlideDeckProgram> _getter;
        public SlideDeckProgramController(ISlideDeckProgramRepo slideDeckProgramRepo) {
            _slideDeckProgramRepo = slideDeckProgramRepo;
            //_getter = _slideDeckProgramRepo as IGetter<SlideDeckProgram>;
        }
        [HttpPost]
        public async Task<IActionResult> Save(SlideDeckProgram slideDeckProgram) {
            //TODO: check if user is authorized
            var result = await _slideDeckProgramRepo.Save(slideDeckProgram);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"api/SlideDeckProgram/{slideDeckProgram.Id}", slideDeckProgram);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsUser() {
            var result = await _slideDeckProgramRepo.GetAllAsUser();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAllAsContentCreator() {
            var result = await _slideDeckProgramRepo.GetAllAsContentCreator();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
