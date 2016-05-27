using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace SolutionTemplate.IdentityServer.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser
                {
                    Username = "JohnDoe",
                    Password = "secret",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Id, "1"),
                        new Claim(Constants.ClaimTypes.Name, "John Doe"),
                        new Claim(Constants.ClaimTypes.PreferredUserName, "jdoe"),
                        new Claim(Constants.ClaimTypes.Email, "jdoe@gmail.com"),
                        new Claim(Constants.ClaimTypes.GivenName, "John"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Doe"),

                        new Claim(Constants.ClaimTypes.Role, "ApiReadWidget"),
                        new Claim(Constants.ClaimTypes.Role, "ApiWriteWidget")
                    }
                },
            };
        }
    }
}