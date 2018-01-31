using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands.Users;
using Gatherer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gatherer.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get() 
            => Json(await _userService.GetAccountAsync(UserId));
        

        [HttpGet("settlements")]
        public async Task<IActionResult> GetSettlements()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(),
                command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command) 
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}