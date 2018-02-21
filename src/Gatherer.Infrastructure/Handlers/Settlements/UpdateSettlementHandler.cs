using System.Threading.Tasks;
using Gatherer.Infrastructure.Commands;
using Gatherer.Infrastructure.Commands.Settlement;
using Gatherer.Infrastructure.Services;

namespace Gatherer.Infrastructure.Handlers.Settlements
{
    public class UpdateSettlementHandler : ICommandHandler<UpdateSettlement>
    {
        private readonly ISettlementService _settlementService;
        public UpdateSettlementHandler(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }
        public async Task HandleAsync(UpdateSettlement command) 
            => await _settlementService.UpdateAsync(command.SettlementId, command.UserId, command.Name, command.Description);
    }
}