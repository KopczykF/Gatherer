using System;
using System.Linq;
using System.Collections.Generic;
using Gatherer.Core.Domain;

namespace Gatherer.Core.Extensions
{
    public static class SettlementExtension
    {
        public static decimal GetAverage(this Settlement settlement)
        {
            var sum = settlement.GetUsersCost().Sum(x => x.Value);
            return sum/settlement.GetUsersCost().Count();
        }

        public static Dictionary<Guid, decimal> GetUsersCost(this Settlement settlement)
        {
            Dictionary<Guid, decimal> usersExpenses = new Dictionary<Guid, decimal>();
            foreach (var User in settlement.UsersExpenseList)
            {
                usersExpenses.Add(User.Key, User.Value.Sum(x => x.Cost));
            }
            return usersExpenses;
        }
    }
}