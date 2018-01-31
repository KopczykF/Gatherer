using System;

namespace Gatherer.Infrastructure.DTO
{
    public class ExpenseDto
    {
        public Guid Id {get; set;}
        public string Name { get;  set; }
        public decimal Cost { get;  set; }
    }
}