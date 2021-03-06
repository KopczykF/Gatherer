using System;

namespace Gatherer.Infrastructure.Commands.Settlement
{
    public class UpdateSettlement : ICommand
    {
        public Guid SettlementId { get; set; }
        public Guid CurrentUserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}