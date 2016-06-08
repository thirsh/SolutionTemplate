using IdentityModel;
using System;
using System.Linq;
using System.Security.Claims;

namespace SolutionTemplate.Core.Claims
{
    public class Claims : IClaims
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public Claims(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public int? Id => GetValue<int?>(JwtClaimTypes.Id);
        public string Username => GetValue<string>(JwtClaimTypes.Name);
        public string FirstName => GetValue<string>(JwtClaimTypes.GivenName);
        public string LastName => GetValue<string>(JwtClaimTypes.FamilyName);
        public string Email => GetValue<string>(JwtClaimTypes.Email);

        private T GetValue<T>(string claimType)
        {
            var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);

            if (string.IsNullOrWhiteSpace(claim?.Value))
            {
                return default(T);
            }

            var t = typeof(T);
            var u = Nullable.GetUnderlyingType(t);

            return (u == null)
                ? (T)Convert.ChangeType(claim.Value, t)
                : (T)Convert.ChangeType(claim.Value, u);
        }
    }
}