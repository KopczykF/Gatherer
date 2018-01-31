using System;
using System.Collections.Generic;

namespace Gatherer.Core.Domain
{
    public class Settlement : Entity
    {
        private Dictionary<Guid, List<Expense>> _usersExpenseList = new Dictionary<Guid, List<Expense>>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<KeyValuePair<Guid, List<Expense>>> UsersExpenseList => _usersExpenseList;

        protected Settlement()
        { }

        public Settlement(Guid id, Guid userId, string name, string description = null)
        {
            Id = id;
            SetName(name);
            SetDescription(Description);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            _usersExpenseList.Add(userId, new List<Expense>());
        }

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception($"Settlement with Id: '{Id}' can not have empty name. ");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        // public void AddExpense(Expense expense)
        // {
        //     if (_usersExpenseList.ContainsKey(expense.UserId))
        //     {
        //         _usersExpenseList[expense.UserId].Add(expense);
        //         UpdatedAt = DateTime.UtcNow;
        //     }
        //     _usersExpenseList.Add(expense.UserId, new List<Expense>{expense});
        //     UpdatedAt = DateTime.UtcNow;
        // }

        public IEnumerable<Expense> GetUserExpenses(Guid userId)
            => _usersExpenseList[userId];
    }
}