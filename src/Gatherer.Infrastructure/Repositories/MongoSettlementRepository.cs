using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Gatherer.Infrastructure.Repositories
{
    public class MongoSettlementRepository : ISettlementRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public MongoSettlementRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Settlement> GetAsync(Guid id)
            => await Settlements.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Settlement>> Browse()
            => await Settlements.AsQueryable().ToListAsync();

        public async Task AddAsync(Settlement settlement)
            => await Settlements.InsertOneAsync(settlement);

        public async Task AddExpenseAsync(Expense expense, Guid settlementId)
        { 
            var settlement = await Settlements.AsQueryable().FirstOrDefaultAsync(x => x.Id == settlementId);
            settlement.AddExpense(expense);
            await Settlements.ReplaceOneAsync(x => x.Id == settlementId, settlement);
        }

        public async Task DeleteExpenseAsync(Guid settlementId, Expense expense)
        {
            var settlement = await Settlements.AsQueryable().FirstOrDefaultAsync(x => x.Id == settlementId);
            settlement.RemoveExpense(expense);
            await Settlements.ReplaceOneAsync(x => x.Id == settlement.Id, settlement);
        }

        public async Task DeleteSettlementAsync(Settlement settlement)
            => await Settlements.DeleteOneAsync(x=> x.Id == settlement.Id);

        public async Task UpdateExpenseAsync(Guid settlementId, Expense expense)
        {
            var settlement = await Settlements.AsQueryable().FirstOrDefaultAsync(x => x.Id == settlementId);
            settlement.UpdateExpense(expense);
            await Settlements.ReplaceOneAsync(x => x.Id == settlement.Id, settlement);
        }

        public async Task UpdateSettlementAsync(Settlement settlement)
            => await Settlements.ReplaceOneAsync(x => x.Id == settlement.Id, settlement);

        private IMongoCollection<Settlement> Settlements => _database.GetCollection<Settlement>("Settlements");
    }
}