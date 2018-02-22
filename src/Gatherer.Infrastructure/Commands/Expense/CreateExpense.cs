using System;

namespace Gatherer.Infrastructure.Commands.Expense
{
    public class CreateExpense : ICommand
    {
        public Guid SettlementId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}