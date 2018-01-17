using System;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface ISettlementService
    {
        Task<SettlementDetailsDto> GetAsync(Guid id);
        Task CreateAsync(Guid id, Guid userId, string name, string description = null);
        Task AddExpenseAsync(Guid id, Guid userId, string name, decimal cost);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);

    }
}