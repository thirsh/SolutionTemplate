﻿using IdentityServer3.Core;
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
                    Username = "Kevin",
                    Password = "secret",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Kevin"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Dockx"),
                        //new Claim(Constants.ClaimTypes.Role, "WebReadUser"),
                        //new Claim(Constants.ClaimTypes.Role, "WebWriteUser"),
                        //new Claim(Constants.ClaimTypes.Role, "MobileReadUser"),
                        //new Claim(Constants.ClaimTypes.Role, "MobileWriteUser")
                    }
                },
                new InMemoryUser
                {
                    Username = "Sven",
                    Password = "secret",
                    Subject = "2",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Sven"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Vercauteren"),
                        //new Claim(Constants.ClaimTypes.Role, "WebReadUser"),
                        //new Claim(Constants.ClaimTypes.Role, "MobileReadUser")
                    }
                },
                new InMemoryUser
                {
                    Username = "Nils",
                    Password = "secret",
                    Subject = "3",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Nils"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Missorten"),
                        //new Claim(Constants.ClaimTypes.Role, "WebWriteUser"),
                        //new Claim(Constants.ClaimTypes.Role, "MobileWriteUser")
                    }
                },
                new InMemoryUser
                {
                    Username = "Kenneth",
                    Password = "secret",
                    Subject = "4",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Kenneth"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Mills"),
                        //new Claim(Constants.ClaimTypes.Role, "WebReadUser"),
                        //new Claim(Constants.ClaimTypes.Role, "WebWriteUser")
                    }
                }
            };
        }
    }
}