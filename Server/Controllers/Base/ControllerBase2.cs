using Learning.Server.Repositories.Base;
using Learning.Server.Service;
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
        IUserService _userService;
        public ControllerBase2(object obj, IUserService userService) {
            RepoBase = obj as IRepoBase3<TEntity>;
            _userService = userService;
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
        public async Task<IActionResult> Ok<T>(Func<Task<T>> task) {
            try {
                var result = await task.Invoke();
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
        public async Task<IActionResult> CreatedIntUri3<T>(Func<Task<Tuple<T,string>>> task, Func<T, string> uri) {
            try {
                var entity = await task.Invoke();
                if (entity.Item1 == null || entity.Item1.Equals(default(T))) {
                    return BadRequest(entity.Item2);
                } else {
                    return Created($"{uri(entity.Item1)}", entity.Item1);
                }
            } catch (Exception ex) {
                var accesslevel = _userService.GetAccessLevel();
                if (accesslevel == Shared.Models.Enums.UserRole.Admin || accesslevel == Shared.Models.Enums.UserRole.ContentCreator) {
                    return Problem(ex.Message);
                }
                return Problem("Sorry, we could not handle this request");
            }
        }

    }
}
