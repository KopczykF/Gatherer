using System;
using System.Collections.Generic;
using Gatherer.Core.Enums;

namespace Gatherer.Core.Domain
{
    public class Settlement : Entity
    {
        private Dictionary<Guid, List<Expense>> _usersExpenseList = new Dictionary<Guid, List<Expense>>();
        private Dictionary<Guid, Dictionary<Guid, decimal>> _usersDebts = new Dictionary<Guid, Dictionary<Guid, decimal>>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public SettleTypesEnum SettleType { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<KeyValuePair<Guid, List<Expense>> > UsersExpenseList => _usersExpenseList;
        public IEnumerable<KeyValuePair<Guid, Dictionary<Guid, decimal>> > UsersDebts => _usersDebts;

        protected Settlement() { }

        public Settlement(Guid id, Guid userId, string name, string description = null, int settleType = 0)
        {
            Id = id;
            SetName(name);
            SetDescription(Description);
            SetSettleType(settleType);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            AddUser(userId);
        }

        public void SetName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception($"Settlement with Id: '{Id}' can not have empty name.");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetSettleType(int settleType)
        {
            //0 because oneTransfer is not implemented
            if (settleType < 0 && settleType > 0)
            {
                throw new Exception("User can not have this settle type");
            }
            switch (settleType)
            {
                case 0:
                    SettleType = SettleTypesEnum.Simple;
                    break;
                case 1:
                    SettleType = SettleTypesEnum.OneTransfer;
                    break;
            }
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddUser(Guid userId)
        {
            if(_usersExpenseList.ContainsKey(userId))
            {
                throw new Exception($"User with Id: {userId} was already added to this settlement.");
            }
            _usersExpenseList.Add(userId, new List<Expense>());
        }

        public void AddExpense(Expense expense)
        {
            if (!_usersExpenseList.ContainsKey(expense.UserId))
            {
                _usersExpenseList.Add(expense.UserId, new List<Expense>());
            }
            _usersExpenseList[expense.UserId].Add(expense);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveExpense(Expense expense)
        {
            if (!_usersExpenseList.ContainsKey(expense.UserId))
            {
                throw new Exception($"User with Id: {expense.Id} have not expenses in this settlement");
            }
            if (!_usersExpenseList[expense.UserId].Contains(expense))
            {
                throw new Exception($"Expense with id: '{expense.Id}' do not exist.");
            }
            _usersExpenseList[expense.UserId].Remove(expense);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateExpense(Expense expense)
        {
            if (!_usersExpenseList.ContainsKey(expense.UserId))
            {
                throw new Exception($"User with Id: {expense.Id} have not expenses in this settlement");
            }
            if (!_usersExpenseList[expense.UserId].Contains(expense))
            {
                throw new Exception($"Expense with id: '{expense.Id}' do not exist.");
            }
            var expenseIndex = _usersExpenseList[expense.UserId].FindIndex(x => x.Id == expense.Id);
            _usersExpenseList[expense.UserId][expenseIndex] = expense;
        }

        public IEnumerable<Expense> GetExpenses(Guid userId)
        {
            if (!_usersExpenseList.ContainsKey(userId))
            {
                throw new Exception($"User with Id: {userId} have not expenses in this settlement");
            }
            return _usersExpenseList[userId]; 
        }

        public Expense GetUserExpense(Guid userId, Guid expenseId)
        {
            if (!_usersExpenseList.ContainsKey(userId))
            {
                throw new Exception($"User with Id: {userId} have not expenses in this settlement");
            }
            if (!_usersExpenseList[expenseId].Exists((x => x.Id == expenseId)))
            {
                throw new Exception($"Expense with id: '{expenseId}' do not exist.");
            }
            return _usersExpenseList[userId].Find((x => x.Id == expenseId));
        }

        public bool HasUser(Guid userId) => _usersExpenseList.ContainsKey(userId);

        public void AddUserDebt(Guid user1, Guid user2, decimal debt)
        {
            if(!_usersDebts.ContainsKey(user1))
            {
                _usersDebts.Add(user1, new Dictionary<Guid, decimal>());
            }
            if (!_usersDebts[user1].ContainsKey(user2))
            {
                _usersDebts[user1].Add(user2, debt);
            }
            _usersDebts[user1][user2] = debt;
        }
    }
}