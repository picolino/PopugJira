using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PopugJira.Services
{
    public class CurrentUserIdentity
    {
        public event Action UserWasSet;
        public User User { get; private set; }
        
        public void SetUserByClaims(IEnumerable<Claim> claims)
        {
            User = new User
                   {
                       Role = claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value switch
                       {
                           "admin" => Role.Admin,
                           "bookkeeper" => Role.Bookkeeper,
                           "manager" => Role.Manager,
                           _ => Role.Programmer
                       }
                   };
            UserWasSet?.Invoke();
        }

        public void Clear()
        {
            User = null;
            UserWasSet?.Invoke();
        }
    }

    public class User
    {
        public Role Role { get; init; }
    }

    public enum Role
    {
        Programmer,
        Bookkeeper,
        Manager,
        Admin
    }
}