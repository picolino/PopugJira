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
            var claimsArray = claims.ToArray();
            User = new User
                   {
                       Name = claimsArray.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                       Role = claimsArray.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value switch
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
        public string Name { get; init; }
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