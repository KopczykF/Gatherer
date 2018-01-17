using System;
using System.Collections.Generic;

namespace Gatherer.Core.Domain
{
    public class Settlement : Entity
    {
        private readonly Dictionary<Guid, List<Expense>> _UsersExpenseList = new Dictionary<Guid, List<Expense>>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<KeyValuePair<Guid, List<Expense>>> UsersExpenseList => _UsersExpenseList;

        protected Settlement() 
        { }

        public Settlement(Guid id, Guid userId, string name, string description = null)
        {
            Id = id;
            SetName(name);
            SetDescription(Description);
            CreateAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            _UsersExpenseList.Add(userId, new List<Expense>());
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
    }
}