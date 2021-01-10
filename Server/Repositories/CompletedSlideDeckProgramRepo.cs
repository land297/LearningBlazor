﻿using Learning.Server.DbContext;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface ICompletedSlideDeckProgramRepo {
        Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram);
        Task<sr<IList<CompletedSlideDeckProgram>>> GetAll();
        Task<sr<IList<CompletedSlideDeckProgram>>> GetAllForUserAvatar(int id);
    }

    public class CompletedSlideDeckProgramRepo : ICompletedSlideDeckProgramRepo {
        private readonly AppDbContext _dbContext;
        readonly IUserService _userService;
        public CompletedSlideDeckProgramRepo(AppDbContext dbContext, IUserService userService) {
            _dbContext = dbContext;
            _userService = userService;
        }
        public async Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram) {
            var response = sr<CompletedSlideDeckProgram>.Get();
            // TOOD : how to handle this, we cannot have existing slideDecks as EF will try to insert them again with same Id
            completedProgram.SlideDeckProgram = null;
            completedProgram.UserAvatar = null;

            try {
                if (completedProgram.Id != default(int)) {
                    var slideDeckProgramInDb = await _dbContext.CompletedSlideDeckPrograms.FirstOrDefaultAsync(x => x.Id == completedProgram.Id);
                    //TODO implement update
                    //slideDeckProgramInDb.CopyValues(completedProgram);
                } else {
                    await _dbContext.CompletedSlideDeckPrograms.AddAsync(completedProgram);
                }
                await _dbContext.SaveChangesAsync();
                response.SetSuccess(completedProgram);
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<IList<CompletedSlideDeckProgram>>> GetAll() {
            var userId = _userService.GetUserId();
            var data = await _dbContext.CompletedSlideDeckPrograms.Where(x => x.UserAvatar.UserId == userId).ToListAsync();
            return sr<IList<CompletedSlideDeckProgram>>.GetSuccess(data);
        }
        public async Task<sr<IList<CompletedSlideDeckProgram>>> GetAllForUserAvatar(int id) {
            var userId = _userService.GetUserId();
            var data = await _dbContext.CompletedSlideDeckPrograms.Where(x => x.UserAvatar.UserId == userId && x.UserAvatar.Id == id).ToListAsync();
            return sr<IList<CompletedSlideDeckProgram>>.GetSuccess(data);
        }
    }
}
