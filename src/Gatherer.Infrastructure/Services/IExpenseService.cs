using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface IExpenseService : IService
    {
         Task<ExpenseDto> GetAsync(Guid settlementId, Guid userId, Guid expenseId, Guid currentUserId);
         Task CreateAsync(Guid settlementId, Guid userId, string name, decimal cost);
         Task UpdateAsync(Guid settlementId, Guid userId, Guid expenseId, string name, decimal cost);
         Task DeleteAsync(Guid settlementId, Guid userId, Guid expenseId);
    }
}