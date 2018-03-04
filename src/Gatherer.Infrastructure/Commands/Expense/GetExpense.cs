using System;

namespace Gatherer.Infrastructure.Commands.Expense
{
    public class GetExpense : ICommand
    {
        public Guid ExpenseId { get; set; }
        public Guid SettlementId { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}