using System;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;

namespace Gatherer.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Settlement> GetOrFailAsync(this ISettlementRepository settlementRepository, Guid id)
        {
            var settlement = await settlementRepository.GetAsync(id);
            if(settlement == null)
            {
                throw new Exception($"Settlement with id: '{id}' does not exist.");
            }

            return settlement;
        }
    }
}