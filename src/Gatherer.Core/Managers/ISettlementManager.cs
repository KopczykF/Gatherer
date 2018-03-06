using Gatherer.Core.Domain;

namespace Gatherer.Core.Managers
{
    public interface ISettlementManager
    {
        void Settle(Settlement settlement);
    }
}