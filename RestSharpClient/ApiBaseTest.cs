using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpClient.DataModels;
using RestSharpClient.Helpers;
using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpClient.Tests
{
    public class ApiBaseTest
    {
        public RestClient RestClient { get; set; }



        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();
        }
    }

}
