using System;
using System.Collections.Generic;
using Gatherer.Core.Domain;

namespace Gatherer.Infrastructure.DTO
{
    public class SettlementDto
    {
        public Guid Id {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string SettleType { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}