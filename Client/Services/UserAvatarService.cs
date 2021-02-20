using Learning.Client.Features;
using Learning.Client.Services.Base;
using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IUserAvatarService {
        Task<sr<UserAvatar>> Get(int id);
        Task<sr<List<UserAvatar>>> GetAll();
        Task<sr<UserAvatar>> Save(UserAvatar userAvatar);
        Task<sr<List<UserAvatar>>> GetAllFor(User user);
        Task<sr<UserAvatar>> SetActive(UserAvatar userAvatar);
        Task<sr<bool>> Delete(UserAvatar userAvatar);
        Task<sr<UserAvatar>> GetActive();
        }

    public class UserAvatarService : IUserAvatarService {
        readonly HttpClient _http;
        ServiceBase2 _base;
        public IMediator Mediator { get; set; }
        public UserAvatarService(HttpClient http, IMediator mediator) {
            _http = http;
            _base = new ServiceBase2(http);
            Mediator = mediator;
        }

        
        public async Task<sr<UserAvatar>> Save(UserAvatar userAvatar) {
            var response =  await _base.Post<UserAvatar, int>("api/userAvatar",userAvatar);
            if (response.Success) {
                userAvatar.Id = response.Data;
                return sr<UserAvatar>.GetSuccess(userAvatar);
            }
            return sr<UserAvatar>.Get(response.Message);
        }
        public async Task<sr<UserAvatar>> SetActive(UserAvatar userAvatar) {
            await Mediator.Send(new ActiveUserAvatarState.ChangeActiveUserAvatarAction { UserAvatar = userAvatar });
            return await _base.Put<int,UserAvatar>($"api/userAvatar/setactive/{userAvatar.Id}", userAvatar.Id);
        }
        public async Task<sr<bool>> Delete(UserAvatar userAvatar) {
            var response = await _base.Delete($"api/userAvatar/{userAvatar.Id}");
            return response;
        }
        public async Task<sr<UserAvatar>> GetActive() {
            var response = await _base.Get<UserAvatar>($"api/useravatar/foruserActive");
            return response;
 
        }
        public async Task<sr<UserAvatar>> Get(int id) {
            var response = await _base.Get<UserAvatar>($"api/useravatar/{id}");
            return response;
            
        }
        public async Task<sr<List<UserAvatar>>> GetAll() {
            var response = await _base.Get<List<UserAvatar>>($"api/useravatar/all");
            return response;
            
        }
        public async Task<sr<List<UserAvatar>>> GetAllFor(User user) {
            var response = await _base.Post<User,List<UserAvatar>>($"api/useravatar/all", user);
            return response;
            
        }
    }
}
