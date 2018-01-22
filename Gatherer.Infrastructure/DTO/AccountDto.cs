using System;

namespace Gatherer.Infrastructure.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}