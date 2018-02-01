using System;
using System.Threading.Tasks;
using AutoMapper;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using Gatherer.Infrastructure.DTO;
using Gatherer.Infrastructure.Extensions;

namespace Gatherer.Infrastructure.Services
{
    public class SettlementService : ISettlementService
    {
        private readonly ISettlementRepository _settlementRepository;
        private readonly IMapper _mapper;

        public SettlementService(ISettlementRepository settlementRepository, IMapper mapper)
        {
            _settlementRepository = settlementRepository;
            _mapper = mapper;
        }

        public async Task<SettlementDetailsDto> GetAsync(Guid id)
        {
            var settlement = await _settlementRepository.GetAsync(id);

            return _mapper.Map<SettlementDetailsDto>(settlement);
        }

        public async Task CreateAsync(Guid id, Guid userId, string name, string description = null)
        {
            var settlement = await _settlementRepository.GetAsync(id);
            if (settlement != null)
            {
                throw new Exception($"Settlement with id: '{id}' already exist.");
            }
            settlement = new Settlement(id, userId, name, description);
            await _settlementRepository.AddAsync(settlement);
        }

        public async Task AddExpenseAsync(Guid settlementId, Guid userId, string name, decimal cost)
        {
            var settlement = await _settlementRepository.GetAsync(settlementId);
            if (settlement == null)
            {
                throw new Exception($"Settlement with id: '{settlementId}' do not exist.");
            }
            var expense = new Expense(Guid.NewGuid(), userId, name, cost);
            await _settlementRepository.AddExpenseAsync(expense, settlementId);
        }

        public async Task RemoveExpenseAsync(Guid settlementId, Guid userId, Guid expenseId)
        {
            var settlement = await _settlementRepository.GetAsync(settlementId);
            if (settlement == null)
            {
                throw new Exception($"Settlement with id: '{settlementId}' do not exist.");
            }
            var userExpense = settlement.GetUserExpense(userId, expenseId);
            await _settlementRepository.RemoveExpenseAsync(userExpense, settlementId);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var settlement = await _settlementRepository.GetOrFailAsync(id);
            settlement.SetName(name);
            settlement.SetDescription(description);
            await _settlementRepository.UpdateAsync(settlement);
        }

        public async Task DeleteAsync(Guid id)
        {
            var settlement = await _settlementRepository.GetOrFailAsync(id);
            await _settlementRepository.DeleteAsync(settlement);
        }
    }
}