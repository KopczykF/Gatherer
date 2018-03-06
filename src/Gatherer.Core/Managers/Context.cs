using Gatherer.Core.Domain;
using Gatherer.Core.Managers.SettleTypes;

namespace Gatherer.Core.Managers
{
    public class Context
    {
        private ISettleType _settleType = null;

        public Context(ISettleType settleType)
        {
            _settleType = settleType;
        }

        public void Calculate(Settlement settlement)
        {
            _settleType.Calculate(settlement);
        }
    }
}