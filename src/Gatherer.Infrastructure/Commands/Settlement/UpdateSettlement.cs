using System;

namespace Gatherer.Infrastructure.Commands.Settlement
{
    public class UpdateSettlement
    {
        public Guid SettlementId {get; set;}
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}