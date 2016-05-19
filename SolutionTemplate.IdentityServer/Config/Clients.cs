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
                    ClientName = "REST Client",
                    ClientId = "20FF5635-2A79-42C2-AD04-40790BE3E14E",
                    Enabled = true,
                    Flow = Flows.Hybrid,
                    RequireConsent = false,

                    //RedirectUris = new List<string> { }
                }
            };
        }
    }
}