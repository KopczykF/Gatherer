using System;

namespace Gatherer.Core.Domain
{
    public class Expense : Entity
    {
        public string Name { get; protected set; }
        public decimal Cost { get; protected set; }

        protected Expense() 
        { }

        public Expense(Guid id, string name, decimal cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
    }
}