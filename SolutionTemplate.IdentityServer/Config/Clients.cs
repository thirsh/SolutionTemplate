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
                    ClientName = "Real Big Client, Inc.",
                    ClientId = "real-big",
                    Enabled = true,
                    Flow = Flows.ResourceOwner,
                    RequireConsent = false,
                    ClientSecrets = new List<Secret> { new Secret("141CA487-D1A5-4246-9AA3-7407712A8F29".Sha256()) },

                    //RedirectUris = new List<string> { }

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        "roles",
                        "solution-template-api"
                    }
                }
            };
        }
    }
}