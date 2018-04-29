using System;
using System.Collections.Generic;
using System.Linq;
using Gatherer.Core.Domain;
using Gatherer.Core.Extensions;

namespace Gatherer.Core.Managers.SettleTypes
{
    public class SimpleSettleType : ISettleType
    {
        public void Calculate(Settlement settlement)
        {
            var average = settlement.GetAverage();
            var usersCost = settlement.GetUsersCost();

            Dictionary<Guid, decimal> usersDebts = new Dictionary<Guid, decimal>();

            foreach (var user in usersCost)
            {
                usersDebts.Add(user.Key, user.Value - average);
            }

            var ordered = usersDebts.OrderBy(x => x.Value);

            foreach (var user1 in ordered)
            {
                if (user1.Value < 0)
                {
                    foreach (var user2 in ordered)
                    {
                        if (user1.Key != user2.Key && user2.Value > 0 && user2.Value + user1.Value >= 0)
                        {
                            settlement.AddUserDebt(user1.Key, user2.Key, -user1.Value);
                            usersDebts[user2.Key] -= user1.Value;
                        }
                    }
                }
            }
        }
    }
}