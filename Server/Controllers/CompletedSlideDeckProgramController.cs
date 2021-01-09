﻿using Learning.Server.Repositories;
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
    public class CompletedSlideDeckProgramController : ControllerBase {
        private readonly ICompletedSlideDeckProgramRepo _completedProgramRepo;

        public CompletedSlideDeckProgramController(ICompletedSlideDeckProgramRepo completedProgramRepo) {
            _completedProgramRepo = completedProgramRepo;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration(CompletedSlideDeckProgram completedProgram) {
            var result = await _completedProgramRepo.Save(completedProgram);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"api/user/{result.Data}", result.Data);
            }
        }
    }
}