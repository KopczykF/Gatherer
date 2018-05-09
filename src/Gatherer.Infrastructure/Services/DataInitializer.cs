using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatherer.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ISettlementService _settlementService;
        private readonly IExpenseService _expenseService;
        public DataInitializer(IUserService userService, ISettlementService settlementService, 
            IExpenseService expenseService)
        {
            _userService = userService;
            _settlementService = settlementService;
            _expenseService = expenseService;
        }
        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();

            if (users.Any())
            {
                return;
            }
            
            var tasks = new List<Task>();
            var user1Guid = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var user2Guid = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var user3Guid = Guid.Parse("00000000-0000-0000-0000-000000000003");
            var settlement1Guid = Guid.Parse("00000000-0000-0000-0000-000000000010");
            var settlement2Guid = Guid.Parse("00000000-0000-0000-0000-000000000020");
            await _userService.RegisterAsync(user1Guid, "user1@email.com", 
                "user1", "secret");
            await _userService.RegisterAsync(user2Guid, "admin@email.com", 
                "user2", "secret", "admin");
            await _userService.RegisterAsync(user3Guid, "user3@email.com", 
                "user3", "secret");
            
            await _settlementService.CreateAsync(settlement1Guid, user1Guid, 
                "Settlement1");
            await _settlementService.CreateAsync(settlement2Guid, user1Guid, 
                "Settlement1");

            await _expenseService.CreateAsync(settlement1Guid, user1Guid, 
                "Expense1Name", 10m);
            await _expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense2Name", 500m);
            await _expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense2Name", 600m);
            await _expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense3Name", 30m);
            await _expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense4Name", 40m);
            await _expenseService.CreateAsync(settlement1Guid, user3Guid, 
                "Expense5Name", 500m);

            await Task.WhenAll(tasks);
            
        }
    }
}