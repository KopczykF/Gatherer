using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Users;
using Gatherer.Infrastructure.Services;

namespace Gatherer.Infrastructure.Handlers.Users
{
    public class RegisterHandler : ICommandHandler<Register>
    {
        private readonly IUserService _userService;

        public RegisterHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Name, command.Password, command.Role);
        }
    }
}