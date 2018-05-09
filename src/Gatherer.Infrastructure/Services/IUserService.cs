using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gatherer.Infrastructure.DTO;

namespace Gatherer.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<AccountDto> GetAccountAsync(Guid currentUserId);
        Task<AccountDto> GetAccountAsync(string email);
        Task<IEnumerable<AccountDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string email, string name,
            string password, string role = "user");

        Task<TokenDto> LoginAsync(string email, string password);
    }
}