using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ISettlementService _settlementService;
        public DataInitializer(IUserService userService, ISettlementService settlementService)
        {
            _userService = userService;
            _settlementService = settlementService;
        }
        public async Task SeedAsync()
        {
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "user1@email.com", 
                "user1", "secret"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "admin@email.com", 
                "user2", "secret", "admin"));
            //TODO add some settlements

            await Task.WhenAll(tasks);
            
        }
    }
}