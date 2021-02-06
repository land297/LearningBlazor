using Learning.Server.Repositories.Base;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers.Base {
    public abstract class ControllerBase2<TEntity> : ControllerBase where TEntity : IdEntity<TEntity> {
        protected IRepoBase3<TEntity> RepoBase;
        public ControllerBase2(object obj) {
            RepoBase = obj as IRepoBase3<TEntity>;
        }

        public async Task<IActionResult> Ok<T>(Task<sr<T>> task) {
            var result = await task;
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        public async Task<IActionResult> Created<T>(Task<sr<T>> task, string uri) {
            var result = await task;
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"{uri}{result.Data}", result.Data);
            }
        }
    }
}
