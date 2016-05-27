using IdentityModel;
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

        public int? Id
        {
            get
            {
                var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);

                return claim != null && !string.IsNullOrWhiteSpace(claim.Value)
                    ? int.Parse(claim.Value)
                    : (int?)null;
            }
        }

        public string Username
        {
            get
            {
                var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name);
                return claim != null ? claim.Value : null;
            }
        }

        public string FirstName
        {
            get
            {
                var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.GivenName);
                return claim != null ? claim.Value : null;
            }
        }

        public string LastName
        {
            get
            {
                var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FamilyName);
                return claim != null ? claim.Value : null;
            }
        }

        public string Email
        {
            get
            {
                var claim = _claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email);
                return claim != null ? claim.Value : null;
            }
        }
    }
}