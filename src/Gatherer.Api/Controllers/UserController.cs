using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Settlement;
using Gatherer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gatherer.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "user")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISettlementService _settlementService;
        public UserController(IUserService userService, ISettlementService settlementService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _settlementService = settlementService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
            => Json(await _userService.GetAccountAsync(UserId));

        [HttpGet("{settlementId}")]
        public async Task<IActionResult> GetSettlement(Guid settlementId)
        {
            var settlement = await _settlementService.GetAsync(settlementId, UserId);
            if (settlement == null)
            {
                return NotFound();
            }

            return Json(settlement);
        }

        [HttpGet("settlements")]
        public async Task<IActionResult> GetSettlements()
        {
            var settlements = await _settlementService.GetSettlementsAsync(UserId);
            if (settlements == null)
            {
                return NotFound();
            }

            return Json(settlements);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateSettlement command)
        {
            command.UserId = UserId;
            await _commandDispatcher.DispatchAsync(command);

            return Created($"/settlement/{command.SettlementId}", null);
        }

        [HttpPut("{settlementId}")]
        public async Task<IActionResult> Put(Guid settlementId, [FromBody]UpdateSettlement command)
        {
            command.UserId = UserId;
            command.SettlementId = settlementId;
            await _commandDispatcher.DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete("{settlementId}")]
        public async Task<IActionResult> Put(Guid settlementId)
        {
            await _settlementService.DeleteAsync(settlementId);

            return NoContent();
        }
    }
}