using Learning.Client.Services.Base;
using Learning.Shared;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface ICompletedReviewableProgramServices {
        Task<sr<CompletedProgramReviewable>> GetAllForUserAvatar();
        Task<sr<List<CompletedProgramReviewable>>> GetUnreviewd();
        Task<sr<CompletedProgramReviewable>> GetUnreviewd(int id);
    }

    public class CompletedReviewableProgramServices : ICompletedReviewableProgramServices {
        private readonly ServiceBase2 _base;
        public CompletedReviewableProgramServices(HttpClient http) {
            _base = new ServiceBase2(http);
        }
        public async Task<sr<List<CompletedProgramReviewable>>> GetUnreviewd() {
            return await _base.Get<List<CompletedProgramReviewable>>("api/CompletedProgramReviewables/unreviewd");
        }
        public async Task<sr<CompletedProgramReviewable>> GetUnreviewd(int id) {
            return await _base.Get<CompletedProgramReviewable>($"api/CompletedProgramReviewables/unreviewd/{id}");
        }
        public async Task<sr<CompletedProgramReviewable>> GetAllForUserAvatar() {
            //return await GetAny(id);
            await Task.Delay(0);
            return null;
        }
    }
}
