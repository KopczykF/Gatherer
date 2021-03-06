using System.Threading.Tasks;
using Gatherer.Core.Domain;
using Gatherer.Core.Enums;
using Gatherer.Core.Managers.SettleTypes;

namespace Gatherer.Core.Managers
{
    public class SettlementManager : ISettlementManager
    {
        private Context context = null;
        public void Settle(Settlement settlement)
        {
            switch (settlement.SettleType)
            {
                case SettleTypesEnum.Simple:
                    context = new Context(new SimpleSettleType());
                    break;
                case SettleTypesEnum.OneTransfer:
                    context = new Context(new OneTransferSettleType());
                    break;
            }

            context.Calculate(settlement);
        }
    }
}