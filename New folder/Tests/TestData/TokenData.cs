using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.DataModels;

namespace RestSharp.Tests.TestData
{
    public class TokenData
    {
        public static TokenUser userTokenDetails()
        {
            return new TokenUser
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
