using HRM.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HRM.Domain.Models.Identity
{
    //For Identity
    public class User : Entity
    {
        public string FirstName { get; private set; } // EF migrations require at least private setter - won't work on auto-property
        public string LastName { get; private set; }
        public string IdentityId { get; private set; }
        public string UserName { get; private set; } // Required by automapper
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        public User() { /* Required by EF */ }

        public User(Guid id, string firstName, string lastName, string identityId, string email, string userName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IdentityId = identityId;
            Email = email;
            UserName = userName;
        }

        public bool HasValidRefreshToken(string refreshToken)
        {
            return _refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
        }

        public void AddRefreshToken(string token, Guid userId, string remoteIpAddress, double daysToExpire = 5)
        {
            _refreshTokens.Add(new RefreshToken(token, DateTime.UtcNow.AddDays(daysToExpire), userId, remoteIpAddress));
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            _refreshTokens.Remove(_refreshTokens.First(t => t.Token == refreshToken));
        }
    }
}
