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

        /*
         *      
    400 Bad Request – This means that client-side input fails validation.
    401 Unauthorized – This means the user isn’t not authorized to access a resource. It usually returns when the user isn’t authenticated.
    403 Forbidden – This means the user is authenticated, but it’s not allowed to access a resource.
    404 Not Found – This indicates that a resource is not found.
    500 Internal server error – This is a generic server error. It probably shouldn’t be thrown explicitly.
    502 Bad Gateway – This indicates an invalid response from an upstream server.
    503 Service Unavailable – This indicates that something unexpected happened on server side (It can be anything like server overload, some parts of the system failed, etc.).
         * 
         */
        [NonAction]
        public async Task<IActionResult> Ok<T>(Task<T> task) {
            try {
                var result = await task;
                if (result == null) {
                    return NotFound();
                } else {
                    return Ok(result);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        public async Task<IActionResult> CreatedObject<T>(Task<T> task, string uri) {
            //TODO: this method return shit..
            try {
                var result = await task;
                if (result == null) {
                    return NotFound();
                } else {
                    return Created($"{uri}{result}", result);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        public async Task<IActionResult> CreatedIntUri(Task<int> task, string uri, object obj) {
            try {
                var id = await task;
                if (id == default(int)) {
                    return NotFound();
                } else {
                    return Created($"{uri}/{id}",obj);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        public async Task<IActionResult> CreatedIntUri2(Task<int> task, Func<int,string> uri, object obj) {
            try {
                var id = await task;
                if (id == default(int)) {
                    return NotFound();
                } else {
                    return Created($"{uri(id)}", obj);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
