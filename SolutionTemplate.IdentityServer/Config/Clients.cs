using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace SolutionTemplate.IdentityServer.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client {
                    ClientName = "HTTP Client",
                    ClientId = "web",
                    Enabled = true,
                    //Flow = Flows.Hybrid,
                    //Flow = Flows.Implicit,
                    Flow = Flows.ResourceOwner,
                    RequireConsent = false,

                    //RedirectUris = new List<string> { }

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "roles",
                        "solution-template-api"
                    }
                }
            };
        }
    }
}