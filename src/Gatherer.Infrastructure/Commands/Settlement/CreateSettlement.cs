using System;
using Gatherer.Core.Domain;

namespace Gatherer.Infrastructure.Commands.Settlement
{
    public class CreateSettlement : ICommand
    {
        public Guid SettlementId {get; set;}
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}