using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Expense;
using Gatherer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Gatherer.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "user")]
    public class ExpenseController : ApiControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMemoryCache _cache;
        public ExpenseController(IExpenseService expenseService, IMemoryCache cache,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _expenseService = expenseService;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> GetExpense([FromBody]GetExpense command)
        {
            command.CurrentUserId = CurrentUserId;
            await _commandDispatcher.DispatchAsync(command);
            
            return Json(_cache.Get(command.ExpenseId));
        }

        [HttpPost("{settlementId}")]
        public async Task<IActionResult> Post([FromBody]CreateExpense command, Guid settlementId)
        {
            command.CurrentUserId = CurrentUserId;
            command.SettlementId = settlementId;
            await _commandDispatcher.DispatchAsync(command);

            return Created($"/settlement/{command.SettlementId}", null);
        }
    }
}