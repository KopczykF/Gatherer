using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface ISettlementService : IService
    {
        Task<SettlementDetailsDto> GetAsync(Guid settlementId, Guid currentUserId);
        Task<IEnumerable<SettlementDetailsDto>> GetSettlementsAsync(Guid currentUserI);
        Task CreateAsync(Guid id, Guid userId, string name, string description = null, int settleType = 0);
        Task UpdateAsync(Guid settlementId, Guid userId, string name, string description);
        Task DeleteAsync(Guid id);

    }
}