using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;

namespace Gatherer.Core.Repositories
{
    public interface ISettlementRepository : IRepository
    {
        Task<Settlement> GetAsync(Guid id);
        Task<IEnumerable<Settlement>> Browse();
        Task AddAsync(Settlement settlement);
        Task AddExpenseAsync(Expense expense, Guid settlementId);
        Task UpdateSettlementAsync(Settlement settlement);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteSettlementAsync(Settlement settlement);
        Task DeleteExpenseAsync(Guid settlementId, Expense expense);
    }
}