using System;
using Gatherer.Core.Domain;

namespace Gatherer.Infrastructure.Commands.Settlement
{
    public class CreateSettlement : ICommand
    {
        public Guid SettlementId { get; set; }
        public Guid CurrentUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}