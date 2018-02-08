using System;
using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Settlement;
using Gatherer.Infrastructure.Commands.Users;
using Gatherer.Infrastructure.Services;

namespace Gatherer.Infrastructure.Handlers.Settlements
{
    public class SettlementHandler: ICommandHandler<CreateSettlement>
    {
        private ISettlementService _settlementService;

        public SettlementHandler(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }
        public async Task HandleAsync(CreateSettlement command)
        {
            //await _settlementService.UpdateAsync(settlementId, command.Name, command.Description);
        }
    }
}