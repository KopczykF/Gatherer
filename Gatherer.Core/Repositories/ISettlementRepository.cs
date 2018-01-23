using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;

namespace Gatherer.Core.Repositories
{
    public interface ISettlementRepository
    {
        Task<Settlement> GetAsync(Guid id);
        // Task<IEnumerable<Settlement>> BrowseAsync(Guid id);
        Task AddAsync(Settlement settlement);
        Task AddExpenseAsync(Expense expense, Guid settlementId);
        Task UpdateAsync(Settlement settlement);
        Task DeleteAsync(Settlement settlement);
    }
}