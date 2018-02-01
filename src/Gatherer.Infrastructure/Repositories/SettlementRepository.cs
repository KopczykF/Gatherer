using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;

namespace Gatherer.Infrastructure.Repositories
{
    public class SettlementRepository : ISettlementRepository
    {
        private static readonly List<Settlement> _settlements = new List<Settlement>();

        public async Task<Settlement> GetAsync(Guid id) 
            => await Task.FromResult(_settlements.SingleOrDefault(x => x.Id == id));

        public async Task AddAsync(Settlement settlement)
        {
            _settlements.Add(settlement);
            await Task.CompletedTask;
        }

        public async Task AddExpenseAsync(Expense expense, Guid settlementId)
        {
            _settlements.Single(x => x.Id == settlementId).AddExpense(expense);
            await Task.CompletedTask;
        }

        public async Task UpdateSettlementAsync(Settlement settlement)
        {
            await Task.CompletedTask;
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteSettlementAsync(Settlement settlement)
        {
            _settlements.Remove(settlement);
            await Task.CompletedTask;
        }

        public async Task DeleteExpenseAsync(Guid settlementId, Expense expense)
        {
            _settlements.Single(x => x.Id == settlementId).RemoveExpense(expense);
            await Task.CompletedTask;
        }
    }
}