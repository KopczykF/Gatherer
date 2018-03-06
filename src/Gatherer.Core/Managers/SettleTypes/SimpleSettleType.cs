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

            Dictionary<Guid, decimal> usersDebs = new Dictionary<Guid, decimal>();

            foreach (var user in usersCost)
            {
                usersDebs.Add(user.Key, user.Value - average);
            }

            var ordered = usersDebs.OrderBy(x => x.Value);

            foreach (var user1 in ordered)
            {
                //skip user1 while he has not debt
                if (user1.Value >= 0)
                {
                    continue;
                }
                foreach (var user2 in ordered)
                {
                    if (user2.Value <= 0 && user2.Value + user1.Value < 0)
                    {
                        continue;
                    }
                    settlement.AddUserDebt(user1.Key, user2.Key, -user1.Value);
                    usersDebs[user2.Key] -= user1.Value;
                }
            }
        }
    }
}