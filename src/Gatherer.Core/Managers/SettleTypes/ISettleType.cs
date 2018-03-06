using Gatherer.Core.Domain;

namespace Gatherer.Core.Managers.SettleTypes
{
    public interface ISettleType
    {
         void Calculate(Settlement settlement);
    }
}