using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;

namespace Gatherer.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Settlement> GetSettlementOrFailAsync(this ISettlementRepository repository, Guid id)
        {
            var settlement = await repository.GetAsync(id);
            if (settlement == null)
            {
                throw new Exception($"Settlement with id: '{id}' does not exist.");
            }

            return settlement;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, string email)
        {
            var user = await repository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"User with email: '{email}' does not exist.");
            }

            return user;
        }

        public static async Task<IEnumerable<Expense>> GetExpensesOrFailAsync(this ISettlementRepository repository, Guid settlementId, Guid userId, Guid currentUserId)
        {
            var settlement = await repository.GetSettlementOrFailAsync(settlementId);
            if (!settlement.HasUser(currentUserId))
            {
                throw new Exception($"User with id: '{currentUserId}' have not expenses in settlement with id: {settlementId}");
            }
            var userExpenses = settlement.GetExpenses(userId);
            if (userExpenses == null)
            {
                throw new Exception($"User with id: '{userId}' have not expenses in settlement with id: {settlementId}");
            }
            return userExpenses;
        }

        public static async Task<IEnumerable<Expense>> GetExpensesOrFailAsync(this ISettlementRepository repository, Guid settlementId, Guid currentUserId)
        {
            var settlement = await repository.GetSettlementOrFailAsync(settlementId);
            var userExpenses = settlement.GetExpenses(currentUserId);
            if (userExpenses == null)
            {
                throw new Exception($"User with id: '{currentUserId}' have not expenses in settlement with id: {settlementId}");
            }
            return userExpenses;
        }

        public static async Task<User> HasAccessToSettlement(this IUserRepository repository, Guid userId, Guid settlementId)
        {
            var user = await repository.GetOrFailAsync(userId);
            if (!user.HasSettlement(settlementId))
            {
                throw new Exception($"This user has not access to settlement with Id: {settlementId}");
            }
            return user;
        }
    }
}