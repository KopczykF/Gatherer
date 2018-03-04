using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Expense;
using Gatherer.Infrastructure.Services;

namespace Gatherer.Infrastructure.Handlers.Expenses
{
    public class CreateExpenseHandler : ICommandHandler<CreateExpense>
    {
        private readonly IExpenseService _expenseService;
        public CreateExpenseHandler(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task HandleAsync(CreateExpense command)
        {
            await _expenseService.CreateAsync(command.SettlementId, command.CurrentUserId, 
                command.Name, command.Cost);
        }
    }
}