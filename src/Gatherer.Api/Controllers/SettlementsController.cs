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
    public class SettlementsController : ApiControllerBase
    {
        private readonly ISettlementService _settlementService;

        public SettlementsController(ISettlementService settlementService, 
            ICommandDispatcher commandDispatcher) : base (commandDispatcher)
        {
            _settlementService = settlementService;
        }

        [HttpGet("{settlementId}")]
        public async Task<IActionResult> Get(Guid settlementid)
        {
            var settlement = await _settlementService.GetAsync(settlementid);
            if(settlement == null)
            {
                return NotFound();
            }

            return Json(settlement);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateSettlement command)
        {
            command.SettlementId = Guid.NewGuid();
            await _settlementService.CreateAsync(command.SettlementId, UserId, 
                command.Name, command.Description);

            return Created($"/settlements/{command.SettlementId}", null);
        }

        [HttpPut("{settlementId}")]
        public async Task<IActionResult> Put(Guid settlementId, [FromBody]UpdateSettlement command)
        {
            await _settlementService.UpdateAsync(settlementId, command.Name, command.Description);

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