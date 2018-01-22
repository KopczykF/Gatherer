using System;
using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Repositories;

namespace Gatherer.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Settlement> GetOrFailAsync(this ISettlementRepository repository, Guid id)
        {
            var settlement = await repository.GetAsync(id);
            if(settlement == null)
            {
                throw new Exception($"Settlement with id: '{id}' does not exist.");
            }

            return settlement;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if(user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }
    }
}