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
    public class SlideDeckController : ControllerBase {
        private readonly ISlideDeckRepo _slideDeckRepo;

        public SlideDeckController(ISlideDeckRepo slideDeckRepo) {
            _slideDeckRepo = slideDeckRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Post(SlideDeck slideDeck) {
            //TODO: check if user is authorized
            var result = await _slideDeckRepo.Add(slideDeck);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created("uri purri prutt",slideDeck);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await _slideDeckRepo.Get();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAsContentCreator() {
            var result = await _slideDeckRepo.GetAsContentCreator();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            // TODO: need to check if user can get unpublished or not
            var result = await _slideDeckRepo.Get(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
