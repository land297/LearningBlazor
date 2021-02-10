using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
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
    public class SlideDeckController : ControllerBase2<SlideDeck> {
        private readonly ISlideDeckRepo _slideDeckRepo;

        public SlideDeckController(ISlideDeckRepo slideDeckRepo) : base (slideDeckRepo){
            _slideDeckRepo = slideDeckRepo;
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Post(SlideDeck slideDeck) {
            //TODO: check if user is authorized
            return await CreatedIntUri(_slideDeckRepo.SaveAndGetId(slideDeck),"api/SlideDeck/",slideDeck);
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsUser() {
            return await Ok(_slideDeckRepo.GetAllAsUser());
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAllAsContentCreator() {
            return await Ok(_slideDeckRepo.GetAllAsContentCreator());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            // TODO: need to check if user can get unpublished or not
            
            return await Ok(_slideDeckRepo.Get(id));
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug) {
            // TODO: need to check if user can get unpublished or not

            return await Ok(_slideDeckRepo.Get(slug));
        }

    }
}
