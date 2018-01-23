using System;

namespace Gatherer.Core.Domain
{
    public class Expense : Entity
    {
        public string Name { get; protected set; }
        public decimal Cost { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Expense() { }

        public Expense(Guid id, Guid userId, string name, decimal cost)
        {
            Id = id;
            UserId = userId;
            SetName(name);
            SetCost(cost);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception($"Expense with Id: '{Id}' can not have empty name. ");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCost(decimal cost)
        {
            if (cost <= 0)
            {
                throw new Exception("Cost can not be less or equal 0.");
            }

            Cost = cost;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUserId(Guid userId)
        {
            if (userId == null)
            {
                throw new Exception("User Id can not be null.");
            }

            UserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}