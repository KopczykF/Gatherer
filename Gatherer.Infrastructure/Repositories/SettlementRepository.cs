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
        private static readonly ISet<Settlement> _settlements = new HashSet<Settlement>();

        public async Task<Settlement> GetAsync(Guid id) 
            => await Task.FromResult(_settlements.SingleOrDefault(x => x.Id == id));

        // public async Task<IEnumerable<Settlement>> BrowseAsync(Guid id)
        //     => await Task.FromResult(_settlements.Where(x => x.Id == id));

        public async Task AddAsync(Settlement settlement)
        {
            _settlements.Add(settlement);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Settlement settlement)
        {
            _settlements.Remove(settlement);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Settlement settlement)
        {
            await Task.CompletedTask;
        }


    }
}