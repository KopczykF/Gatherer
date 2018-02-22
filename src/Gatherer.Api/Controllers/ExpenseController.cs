using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Expense;
using Gatherer.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gatherer.Api.Controllers
{
    [Route("{settlementId}/[controller]")]
    [Authorize(Policy = "user")]
    public class ExpenseController : ApiControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _expenseService = expenseService;
        }

        [HttpGet("{expenseId}")]
        public async Task<IActionResult> GetExpense(Guid settlementId, Guid expenseId)
        {
            var expense = await _expenseService.GetAsync(settlementId, UserId, expenseId);

            return Json(expense);
        }

        [HttpGet]
        public async Task<IActionResult> GetForSettlement(Guid settlementId)
        {
            var expenses = await _expenseService.GetForUserAsync(settlementId, UserId);

            return Json(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid settlementId, [FromBody]CreateExpense command)
        {
            command.UserId = UserId;
            command.SettlementId = settlementId;
            await _commandDispatcher.DispatchAsync(command);

            return Created($"/settlement/{command.SettlementId}", null);
        }
    }
}