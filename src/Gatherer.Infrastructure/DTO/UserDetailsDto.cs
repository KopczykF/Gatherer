using System;
using System.Collections.Generic;

namespace Gatherer.Infrastructure.DTO
{
    public class UserDetailsDto
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateAd { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<Guid> UserSettlements  { get; set; }
        public IEnumerable<Guid> UserFriends  { get; set; }
        
    }
}