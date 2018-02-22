using System;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Users;
using Gatherer.Infrastructure.Services;
using Gatherer.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Gatherer.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public AccountController(IUserService userService, IMemoryCache cache,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] Register command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            command.TokenId = Guid.NewGuid();
            await _commandDispatcher.DispatchAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);

            return Json(jwt);
        }
    }
}