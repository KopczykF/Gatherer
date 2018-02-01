using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;
using Gatherer.Infrastructure.DTO;
using Gatherer.Infrastructure.Extensions;

namespace Gatherer.Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ISettlementRepository _settlementRepository;
        private readonly IMapper _mapper;

        public ExpenseService(ISettlementRepository settlementRepository, IMapper mapper)
        {
            _settlementRepository = settlementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDto>> GetForUserAsync(Guid settlementId, Guid userId)
        {
            var userExpenses = await _settlementRepository.GetUserExpensesOrFailAsync(settlementId,userId);

            return _mapper.Map<IEnumerable<ExpenseDto>>(userExpenses);
        }

        public async Task<ExpenseDto> GetAsync(Guid settlementId, Guid userId, Guid expenseId)
        {
            var userExpenses = await _settlementRepository.GetUserExpensesOrFailAsync(settlementId,userId);
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
        }

        public async Task UpdateAsync(Guid settlementId, Guid userId, Guid expenseId, string name, decimal cost)
        {
            var userExpenses = await _settlementRepository.GetUserExpensesOrFailAsync(settlementId,userId);
            var expense = userExpenses.SingleOrDefault(x => x.Id == expenseId);
            if (userExpenses == null)
            {
                throw new Exception($"Expense with id: '{expenseId}' does not exist.");
            }
            expense.SetName(name);
            expense.SetCost(cost);

            await _settlementRepository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteAsync(Guid settlementId, Guid userId, Guid expenseId)
        {
            var settlement = await _settlementRepository.GetSettlementOrFailAsync(settlementId);
            var userExpense = settlement.GetUserExpense(userId, expenseId);
            await _settlementRepository.DeleteExpenseAsync(settlementId, userExpense);
        }
    }
}