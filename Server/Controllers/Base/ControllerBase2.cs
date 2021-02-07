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

        public async Task<IActionResult> Ok<T>(Task<T> task) {
            try {
                var result = await task;
                if (result == null) {
                    return Ok(sr<T>.Get("Result was null"));
                } else {
                    return Ok(sr<T>.GetSuccess(result));
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Created<T>(Task<T> task, string uri) {
            try {
                var result = await task;
                if (result == null) {
                    return Ok(sr<T>.Get("Result was null"));
                } else {
                    return Created($"{uri}{result}", result);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
