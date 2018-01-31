using System;
using System.Collections.Generic;

namespace Gatherer.Infrastructure.DTO
{
    public class SettlementDetailsDto : SettlementDto
    {
        public IEnumerable<KeyValuePair<Guid, List<ExpenseDto>>> UsersExpenseList {get; set;}
    }
}