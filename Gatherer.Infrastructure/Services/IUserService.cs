using System;
using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string email, string name, 
            string password, string role = "user");

        Task LoginAsync(string email, string password);
    }
}