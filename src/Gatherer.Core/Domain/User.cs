using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Gatherer.Core.Domain
{
    public class User : Entity
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        private static readonly Regex EmailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        private readonly ISet<Guid> _userSettlements = new HashSet<Guid>();
        private readonly ISet<Guid> _userFriends = new HashSet<Guid>();
        private static List<string> _roles = new List<string>
        {
            "user", "admin"
        };
        public string Role { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreateAd { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Guid> UserSettlements => _userSettlements;
        public IEnumerable<Guid> UserFriends => _userFriends;
        

        protected User()
        { }

        public User(Guid id, string role, string name, string email,
            string password, string salt)
        {
            Id = id;
            SetRole(role);
            SetUsername(name);
            SetEmail(email);
            SetPassword(password, salt);
            CreateAd = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }

            if (String.IsNullOrEmpty(username))
            {
                throw new Exception("Username is invalid.");
            }

            Name = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email can not be empty.");
            }
            if (!EmailRegex.IsMatch(email))
            {
                throw new Exception("Email is invalid.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                role = "user";
            }
            if (!_roles.Contains(role))
            {
                throw new Exception($"User can not have a role: '{role}.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("Salt can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password can not contain more than 100 characters.");
            }
            if (Password == password)
            {
                return;
            }
            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddFriend(Guid friendId)
            => _userFriends.Add(friendId);

        public void RemoveFriend(Guid friendId)
            => _userFriends.Remove(friendId);

        public void AddSettlement(Guid settlementId)
            => _userSettlements.Add(settlementId);

        public void RemoveSettlement(Guid settlementId)
            => _userSettlements.Remove(settlementId);

        public bool HasSettlement(Guid settlementId)
            => _userSettlements.Contains(settlementId);
    }
}