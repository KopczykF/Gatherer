using System;
using Gatherer.Core.Domain;

namespace Gatherer.Infrastructure.Commands.Settlement
{
    public class CreateSettlement : ICommand
    {
        public Guid SettlementId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}