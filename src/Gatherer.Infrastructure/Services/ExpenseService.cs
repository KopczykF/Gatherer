using System;
using AutoMapper;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using Gatherer.Infrastructure.DTO;
using Gatherer.Infrastructure.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gatherer.Core.Managers;

namespace Gatherer.Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ISettlementRepository _settlementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private ISettlementManager manager = new SettlementManager();

        public ExpenseService(ISettlementRepository settlementRepository, IUserRepository userRepository, IMapper mapper)
        {
            _settlementRepository = settlementRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> GetAsync(Guid settlementId, Guid userId, Guid expenseId, Guid currentUserId)
        {
            var userExpenses = await _settlementRepository.GetExpensesOrFailAsync(settlementId, userId, currentUserId);
            var expense = userExpenses.SingleOrDefault(x => x.Id == expenseId);

            if (userExpenses == null)
            {
                throw new Exception($"Expense with id: '{expenseId}' does not exist.");
            }
            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task CreateAsync(Guid settlementId, Guid userId, string name, decimal cost)
        {
            var settlement = await _settlementRepository.GetSettlementOrFailAsync(settlementId);
            var expense = new Expense(Guid.NewGuid(), userId, name, cost);
            await _settlementRepository.AddExpenseAsync(expense, settlementId);
            var user = await _userRepository.GetAsync(userId);
            user.AddSettlement(settlementId);
            manager.Settle(settlement);
        }

        public async Task UpdateAsync(Guid settlementId, Guid userId, Guid expenseId, string name, decimal cost)
        {
            var userExpenses = await _settlementRepository.GetExpensesOrFailAsync(settlementId, userId);
            var expense = userExpenses.SingleOrDefault(x => x.Id == expenseId);
            if (userExpenses == null)
            {
                throw new Exception($"Expense with id: '{expenseId}' does not exist.");
            }
            expense.SetName(name);
            expense.SetCost(cost);
            manager.Settle(await _settlementRepository.GetAsync(settlementId));

            await _settlementRepository.UpdateExpenseAsync(settlementId, expense);
        }

        public async Task DeleteAsync(Guid settlementId, Guid userId, Guid expenseId)
        {
            var settlement = await _settlementRepository.GetSettlementOrFailAsync(settlementId);
            var userExpense = settlement.GetUserExpense(userId, expenseId);
            await _settlementRepository.DeleteExpenseAsync(settlementId, userExpense);
            var user = await _userRepository.GetAsync(userId);
            user.RemoveSettlement(settlementId);
        }
    }
}