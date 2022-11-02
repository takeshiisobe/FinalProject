using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.DataModels;
using RestSharp.Helpers;


namespace RestSharp.Tests
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
