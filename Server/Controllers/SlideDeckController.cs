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
        private readonly IAzureRepo _azureRepo;
        private readonly IUserService _userService;
        private readonly IUserAvatarRepo _userAvatar;
        IVideoRepo _videos;
        public SlideDeckController(ISlideDeckRepo slideDeckRepo,
             IUserService us, IVideoRepo v, IAzureRepo azureRepo, IUserService userService,IUserAvatarRepo userAvatarRepo) : base (slideDeckRepo,us){
            _slideDeckRepo = slideDeckRepo;
            _azureRepo = azureRepo;
            _videos = v;
            _userService = userService;
            _userAvatar = userAvatarRepo;
        }
        [Authorize(Roles = "Admin,ContentCreator")]
        [HttpPost]
        public async Task<IActionResult> Post(SlideDeck slideDeck) {
            //TODO: check if user is authorized
            foreach (var slide in slideDeck.Slides) {
                var k = slide.TextContent.Split("scr=");
                for (int i = 1; i < k.Length; i++) {
                    var sasurl = k[i].Split(" ").First();
                    var url = sasurl.Split("?").First();
                    k[i] = "scr=" + k[i].Replace(sasurl, url).Replace("\"", "");
                }
                slide.TextContent = string.Join(null,k);
            }
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
            
            //return await TryOk(() => _slideDeckRepo.Get(id));
            return await TryOk(async () => {
                var deck = await _slideDeckRepo.Get(id);

                var activeAvatar =  await _userAvatar.GetActiveInContext();

                if (deck.AccessLevel == Shared.Models.Enums.AccessLevel.Premium &&
                (_userService.GetAccessLevel() != Shared.Models.Enums.UserRole.Premium))
                //|| activeAvatar.PersonalProgramAccess.Count(x => x.SlideDeckProgramId == deck.Id) == 0)
                {
                    // does not have access. return dummy deck!
                    var dummy = await _slideDeckRepo.Get(10);
                    dummy.Title = deck.Title;
                    dummy.Description = deck.Description + Environment.NewLine + Environment.NewLine + dummy.Description;
                    
                    return dummy;
                }
                foreach (var slide in deck.Slides.Where(s => !string.IsNullOrWhiteSpace(s.TextContent))) {
                    var k = slide.TextContent.Split("scr=");
                    for (int i = 1; i < k.Length; i++) {
                        var url = k[i].Split(" ").First().Replace("\"","");
                        var sasurl = _azureRepo.GetSasUriForBlob(new Uri(url)).ToString();
                        //.Replace("&", "&amp;");
                        k[i] = "scr=" + k[i].Replace(url,sasurl);
                    }
                    slide.TextContent = string.Join(null, k);
                 }
                return deck;
            });
        }
        //[HttpGet("{slug}")]
        //public async Task<IActionResult> Get(string slug) {
        //    // TODO: need to check if user can get unpublished or not

        //    return await TryOk(() => _slideDeckRepo.Get(slug));
        //}

    }
}
