using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Expense;
using Gatherer.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Gatherer.Infrastructure.Handlers.Expenses
{
    public class GetExpenseHandler : ICommandHandler<GetExpense>
    {
        private readonly IExpenseService _expenseService;
        private readonly IMemoryCache _cache;

        public GetExpenseHandler(IExpenseService expenseService, IMemoryCache cache)
        {
            _expenseService = expenseService;
            _cache = cache;
        }

        public async Task HandleAsync(GetExpense command)
        {
            var expense = await _expenseService.GetAsync(command.SettlementId, 
                command.UserId, command.ExpenseId, command.CurrentUserId);
            _cache.Set(command.ExpenseId, expense, TimeSpan.FromSeconds(5));
        }
    }
}