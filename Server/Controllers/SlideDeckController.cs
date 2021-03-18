using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Learning.Shared.DataTransferModel;
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
        IVideoRepo _videos;
        public SlideDeckController(ISlideDeckRepo slideDeckRepo,
             IUserService us, IVideoRepo v) : base (slideDeckRepo,us){
            _slideDeckRepo = slideDeckRepo;
            _videos = v;
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Post(SlideDeck slideDeck) {
            //TODO: check if user is authorized
            return await CreatedIntUri3<int>(() =>_slideDeckRepo.SaveAndGetId(slideDeck),(id) => "api/SlideDeck/" + id);
            
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost("file")]
        public async Task<IActionResult> Post(FileUpload file) {
            //TODO: check if user is authorized
            _videos.file = file;
            var url = await _videos.SaveIamgeToFtp();
            return Ok(url);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsUser() {
            return await TryOk(() => _slideDeckRepo.GetAllAsUser());
        }
        [HttpGet("contentcreator")]
        public async Task<IActionResult> GetAllAsContentCreator() {
            return await TryOk(() => _slideDeckRepo.GetAllAsContentCreator());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            // TODO: need to check if user can get unpublished or not
            
            return await TryOk(() => _slideDeckRepo.Get(id));
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug) {
            // TODO: need to check if user can get unpublished or not

            return await TryOk(() => _slideDeckRepo.Get(slug));
        }

    }
}
