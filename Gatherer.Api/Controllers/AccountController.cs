using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands.Users;
using Gatherer.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gatherer.Api.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("settlements")]
        public async Task<IActionResult> GetSettlements()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), 
                command.Email, command.UserName, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}