using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace SolutionTemplate.IdentityServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,

                new Scope
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Enabled = true,
                    Description = "The roles you belong to.",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope
                {
                    Name = "solutionTemplateApi",
                    DisplayName = "Solution Template API Scope",
                    Enabled = true,
                    Type = ScopeType.Resource,
                    Emphasize = false,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };
        }
    }
}