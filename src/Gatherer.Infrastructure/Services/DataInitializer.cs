using System;
using System.Collections.Generic;
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
            var tasks = new List<Task>();
            var user1Guid = Guid.NewGuid();
            var user2Guid = Guid.NewGuid();
            var settlement1Guid = Guid.NewGuid();
            var settlement2Guid = Guid.NewGuid();
            tasks.Add(_userService.RegisterAsync(user1Guid, "user1@email.com", 
                "user1", "secret"));
            tasks.Add(_userService.RegisterAsync(user2Guid, "admin@email.com", 
                "user2", "secret", "admin"));
            
            tasks.Add(_settlementService.CreateAsync(settlement1Guid, user1Guid, 
                "Settlement1"));
            tasks.Add(_settlementService.CreateAsync(settlement2Guid, user1Guid, 
                "Settlement1"));

            tasks.Add(_expenseService.CreateAsync(settlement1Guid, user1Guid, 
                "Expense1Name", 10m));
            tasks.Add(_expenseService.CreateAsync(settlement1Guid, user1Guid, 
                "Expense2Name", 20m));
            tasks.Add(_expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense3Name", 30m));
            tasks.Add(_expenseService.CreateAsync(settlement1Guid, user2Guid, 
                "Expense4Name", 40m));

            await Task.WhenAll(tasks);
            
        }
    }
}