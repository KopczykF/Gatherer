using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface ISettlementService
    {
        Task<SettlementDetailsDto> GetAsync(Guid id);
        // Task<IEnumerable<SettlementDetailsDto>> GetForUserAsync(Guid userId);
        Task CreateAsync(Guid id, Guid userId, string name, string description = null);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);

    }
}