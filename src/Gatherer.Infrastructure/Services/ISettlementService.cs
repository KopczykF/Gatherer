using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface ISettlementService
    {
        Task<SettlementDetailsDto> GetAsync(Guid settlementId, Guid userId);
        Task CreateAsync(Guid id, Guid userId, string name, string description = null);
        Task UpdateAsync(Guid settlementId, Guid userId, string name, string description);
        Task DeleteAsync(Guid id);

    }
}