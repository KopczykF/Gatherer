using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Settlement;
using Gatherer.Infrastructure.Services;

namespace Gatherer.Infrastructure.Handlers.Settlements
{
    public class CreateSettlementHandler : ICommandHandler<CreateSettlement>
    {
        private readonly ISettlementService _settlementService;
        public CreateSettlementHandler(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }

        public async Task HandleAsync(CreateSettlement command)
        {
            command.SettlementId = Guid.NewGuid();
            await _settlementService.CreateAsync(command.SettlementId, command.CurrentUserId, 
                command.Name, command.Description, command.SettleType);
        }
    }
}